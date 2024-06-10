using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityUtils;


namespace DannyG
{
	
	public class WinChecker : Singleton<WinChecker>
	{
		[SerializeField, Min(0)] private int turnCountWinCheckingThreshold = 7;
		
		private int[,] _board;
		private int _maxMoveNumber;


		private void Start()
		{
			EventManager.onPlacePiece.Subscribe(ReceivePlacePieceEvent);
			EventManager.onApplyGravityShiftToDisplay.Subscribe(CheckForWinOnWholeBoard);
			CalculateMaxMoveNumber();
		}
		private void OnDestroy()
		{
			EventManager.onPlacePiece.Unsubscribe(ReceivePlacePieceEvent);
			EventManager.onApplyGravityShiftToDisplay.Unsubscribe(CheckForWinOnWholeBoard);
		}
		private void CalculateMaxMoveNumber()
		{
			_board = BoardStateManager.grid;
			_maxMoveNumber = 0;
			for (var y = 0; y < _board.GetLength(1); y++)
			{
				for (var x = 0; x < _board.GetLength(0); x++)
				{
					if (_board[x, y] == (int)TileType.Empty)
					{
						_maxMoveNumber++;
					}
				}
			}
		}

		private async Task StartOfWinCheck()
		{
			if (IsWinCheckThresholdReached()) return;
			await Task.Yield(); // wait for board state to update // make sure that the board display update event is sent after this method
			_board = BoardStateManager.grid;
		}
		
		private async void ReceivePlacePieceEvent(MoveData moveData)
		{
			await StartOfWinCheck();
			if (CheckForWinAroundPiece(moveData.Coordinate, BoardStateManager.boardState))
			{
				EventManager.onPlayerWin.Invoke(moveData.PlayerId);
				return;
			}
			//Check for draw after checking for win
			CheckForDraw();
		}

		public bool CheckForWinAroundPiece(Coordinate coordinate, BoardState boardState)
		{
			return LineOfPiecesOperations.GetLongestLineOfTilesInArea(coordinate, boardState) >= 4;
		}
		
		private async void CheckForWinOnWholeBoard(AllShiftedTilesData allShiftedTiles)
		{
			await StartOfWinCheck(); // temporary while the other algorithm is not implemented
			WholeBoardWinCheck(BoardStateManager.boardState);
		}

		public bool WholeBoardWinCheck(BoardState boardState)
		{
			_board = boardState.grid;
			int currentNumberOfPiecesInALine = 0;
			bool isPlayer1Line = default;
			bool isThereAWinner = false;
			
			isThereAWinner = LoopHorizontally();
			if (isThereAWinner) return true;
			isThereAWinner = LoopVertically();
			if (isThereAWinner) return true;
			isThereAWinner = LoopForwardDiagonally();
			if (isThereAWinner) return true;
			isThereAWinner = LoopBackwardsDiagonally();
			return isThereAWinner;

			void EditNumberOfPiecesInALine(int currentX, int currentY)
			{
				switch (_board[currentX, currentY])
				{
					case (int)TileType.Empty:
					case (int)TileType.Blocker:
						currentNumberOfPiecesInALine = 0;
						break;
					case (int)TileType.Player1Token:
						if (isPlayer1Line == false || currentNumberOfPiecesInALine == 0)
						{
							currentNumberOfPiecesInALine = 1;
							isPlayer1Line = true;
						}
						else
							currentNumberOfPiecesInALine++;
						break;
					case (int)TileType.Player2Token:
						if (isPlayer1Line || currentNumberOfPiecesInALine == 0)
						{
							currentNumberOfPiecesInALine = 1;
							isPlayer1Line = false;
						}
						else
							currentNumberOfPiecesInALine++;
						break;
				}
			}

			bool CheckForWin()
			{
				if (currentNumberOfPiecesInALine == 4)
				{
					EventManager.onPlayerWin.Invoke(isPlayer1Line ? PlayerId.Player1 : PlayerId.Player2);
					return true;
				}
				return false;
			}

			bool LoopHorizontally()
			{
				for (int y = 0; y < _board.GetLength(1); y++)
				{
					currentNumberOfPiecesInALine = 0;
					for (int x = 0; x < _board.GetLength(0); x++)
					{
						EditNumberOfPiecesInALine(x, y);
						if (CheckForWin()) return true;
					}
				}
				return false;
			}

			bool LoopVertically()
			{
				for (int x = 0; x < _board.GetLength(0); x++)
				{
					currentNumberOfPiecesInALine = 0;
					for (int y = 0; y < _board.GetLength(1); y++)
					{
						EditNumberOfPiecesInALine(x, y);
						if (CheckForWin()) return true;
					}
				}
				return false;
			}

			bool LoopForwardDiagonally() // diagonally like a forward slash
			{
				int xLength = _board.GetLength(0);
				int yLength = _board.GetLength(1);
				const int cutOff = 3; // don't check diagonals that have 3 elements or less
				
				for (int y = cutOff; y < yLength; y++)
				{
					currentNumberOfPiecesInALine = 0;
					int row = y;
					int col = 0;
					while (row >= 0)
					{
						EditNumberOfPiecesInALine(row, col);
						if (CheckForWin()) return true;
						row--;
						col++;
					}
				}
				for (int x = 1; x < xLength - cutOff; x++)
				{
					currentNumberOfPiecesInALine = 0;
					int row = yLength - 1;
					int col = x;
					while (col < xLength)
					{
						EditNumberOfPiecesInALine(row, col);
						if (CheckForWin()) return true;
						row--;
						col++;
					}
				}
				return false;
			}
			
			bool LoopBackwardsDiagonally() // diagonally like a back slash
			{
				int xLength = _board.GetLength(0);
				int yLength = _board.GetLength(1);
				const int cutOff = 3; // don't check diagonals that have 3 elements or less
				
				for (int y = cutOff; y < yLength; y++)
				{
					currentNumberOfPiecesInALine = 0;
					int row = y;
					int col = xLength - 1;
					while (row >= 0)
					{
						EditNumberOfPiecesInALine(row, col);
						if (CheckForWin()) return true;
						row--;
						col--;
					}
				}
				for (int x = xLength - 2; x >= cutOff; x--)
				{
					currentNumberOfPiecesInALine = 0;
					int row = yLength - 1;
					int col = x;
					while (col >= 0)
					{
						EditNumberOfPiecesInALine(row, col);
						if (CheckForWin()) return true;
						row--;
						col--;
					}
				}
				return false;
			}
		}
		
		
		private void CheckForDraw()
		{
			if (TurnManager.Instance.turnCount == _maxMoveNumber)
			{
				EventManager.onDrawGame.Invoke();
			}
		}

		public bool IsDrawState(BoardState boardState, int currentDepth)
		{
			// Check if there are no empty tiles in the grid
			// This gives exception. IList doesn't work with 2D arrays
			//return ((IList)boardState.grid).Contains((int)TileType.Empty) == false;

			int turnCount = TurnManager.Instance.turnCount + currentDepth;
			return turnCount == _maxMoveNumber;
		}

		private bool IsWinCheckThresholdReached()
		{
			return TurnManager.Instance.turnCount < turnCountWinCheckingThreshold;
		}
		public bool IsWinCheckThresholdReached(int currentDepth)
		{
			int turnCount = TurnManager.Instance.turnCount + currentDepth;
			return turnCount < turnCountWinCheckingThreshold;
		}

	}
}
