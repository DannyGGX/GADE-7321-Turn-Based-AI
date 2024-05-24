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
		private int _currentNumberOfPiecesInALine;


		private void Start()
		{
			EventManager.onPlacePiece.Subscribe(CheckForWinAroundPiece);
			EventManager.onApplyGravityShiftToDisplay.Subscribe(CheckForWinOnWholeBoard);
			CalculateMaxMoveNumber();
		}
		private void OnDestroy()
		{
			EventManager.onPlacePiece.Unsubscribe(CheckForWinAroundPiece);
			EventManager.onApplyGravityShiftToDisplay.Unsubscribe(CheckForWinOnWholeBoard);
		}
		private void CalculateMaxMoveNumber()
		{
			_board = BoardStateManager.Instance.grid;
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
			if (TurnManager.Instance.turnCount < turnCountWinCheckingThreshold) return;
			await Task.Yield(); // wait for board state to update // make sure that the board display update event is sent after this method
			_board = BoardStateManager.Instance.grid;
		}
		
		private async void CheckForWinAroundPiece(MoveData moveData)
		{
			await StartOfWinCheck();
			
			WholeBoardWinCheck(); // temporary while the other algorithm is not implemented
			//Check after checking for win
			CheckForDraw();
		}
		
		private async void CheckForWinOnWholeBoard(AllShiftedTilesData allShiftedTiles)
		{
			await StartOfWinCheck(); // temporary while the other algorithm is not implemented
			WholeBoardWinCheck();
		}

		private void WholeBoardWinCheck()
		{
			_currentNumberOfPiecesInALine = 0;
			bool isPlayer1Line = default;
			
			LoopHorizontally();
			LoopVertically();
			LoopForwardDiagonally();
			LoopBackwardsDiagonally();
			return;

			void EditNumberOfPiecesInALine(int currentX, int currentY)
			{
				switch (_board[currentX, currentY])
				{
					case (int)TileType.Empty:
					case (int)TileType.Blocker:
						_currentNumberOfPiecesInALine = 0;
						break;
					case (int)TileType.Player1Token:
						if (isPlayer1Line == false || _currentNumberOfPiecesInALine == 0)
						{
							_currentNumberOfPiecesInALine = 1;
							isPlayer1Line = true;
						}
						else
							_currentNumberOfPiecesInALine++;
						break;
					case (int)TileType.Player2Token:
						if (isPlayer1Line || _currentNumberOfPiecesInALine == 0)
						{
							_currentNumberOfPiecesInALine = 1;
							isPlayer1Line = false;
						}
						else
							_currentNumberOfPiecesInALine++;
						break;
				}
			}

			bool CheckForWin()
			{
				if (_currentNumberOfPiecesInALine == 4)
				{
					EventManager.onPlayerWin.Invoke(isPlayer1Line ? PlayerId.Player1 : PlayerId.Player2);
					return true;
				}
				return false;
			}

			void LoopHorizontally()
			{
				for (int y = 0; y < _board.GetLength(1); y++)
				{
					_currentNumberOfPiecesInALine = 0;
					for (int x = 0; x < _board.GetLength(0); x++)
					{
						EditNumberOfPiecesInALine(x, y);
						if (CheckForWin()) return;
					}
				}
			}

			void LoopVertically()
			{
				for (int x = 0; x < _board.GetLength(0); x++)
				{
					_currentNumberOfPiecesInALine = 0;
					for (int y = 0; y < _board.GetLength(1); y++)
					{
						EditNumberOfPiecesInALine(x, y);
						if (CheckForWin()) return;
					}
				}
			}

			void LoopForwardDiagonally() // diagonally like a forward slash
			{
				int xLength = _board.GetLength(0);
				int yLength = _board.GetLength(1);
				const int cutOff = 3; // don't check diagonals that have 3 elements or less
				
				for (int y = cutOff; y < yLength; y++)
				{
					_currentNumberOfPiecesInALine = 0;
					int row = y;
					int col = 0;
					while (row >= 0)
					{
						EditNumberOfPiecesInALine(row, col);
						if (CheckForWin()) return;
						row--;
						col++;
					}
				}
				for (int x = 1; x < xLength - cutOff; x++)
				{
					_currentNumberOfPiecesInALine = 0;
					int row = yLength - 1;
					int col = x;
					while (col < xLength)
					{
						EditNumberOfPiecesInALine(row, col);
						if (CheckForWin()) return;
						row--;
						col++;
					}
				}
			}
			
			void LoopBackwardsDiagonally() // diagonally like a back slash
			{
				int xLength = _board.GetLength(0);
				int yLength = _board.GetLength(1);
				const int cutOff = 3; // don't check diagonals that have 3 elements or less
				
				for (int y = cutOff; y < yLength; y++)
				{
					_currentNumberOfPiecesInALine = 0;
					int row = y;
					int col = xLength - 1;
					while (row >= 0)
					{
						EditNumberOfPiecesInALine(row, col);
						if (CheckForWin()) return;
						row--;
						col--;
					}
				}
				for (int x = xLength - 2; x >= cutOff; x--)
				{
					_currentNumberOfPiecesInALine = 0;
					int row = yLength - 1;
					int col = x;
					while (col >= 0)
					{
						EditNumberOfPiecesInALine(row, col);
						if (CheckForWin()) return;
						row--;
						col--;
					}
				}
			}
		}
		
		
		private void CheckForDraw()
		{
			if (TurnManager.Instance.turnCount == _maxMoveNumber)
			{
				EventManager.onDrawGame.Invoke();
			}
		}

	}
}
