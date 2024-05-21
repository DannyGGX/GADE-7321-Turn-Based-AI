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
		
		private async void CheckForWinAroundPiece(MoveData moveData)
		{
			if (TurnManager.Instance.turnCount < turnCountWinCheckingThreshold) return;
			await Task.Yield(); // wait for board to update // make sure that the board update event is sent after this method
			_board = BoardStateManager.Instance.grid;
			
			
			//Check after checking for win
			CheckForDraw();
		}
		
		private async void CheckForWinOnWholeBoard(AllShiftedTilesData allShiftedTiles)
		{
			if (TurnManager.Instance.turnCount < turnCountWinCheckingThreshold) return;
			await Task.Yield(); // wait for board to update // make sure that the board update event is sent after this method
			_board = BoardStateManager.Instance.grid;
			int currentNumberOfPiecesInALine = 0;
			bool isPlayer1Line = default;
			
			LoopHorizontally();
			LoopVertically();
			LoopForwardDiagonally();
			LoopBackwardsDiagonally();
			CheckForDraw(); // temporary while the other algorithm is not implemented

			void AddToNumberOfPiecesInALine(int currentX, int currentY)
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

			void LoopHorizontally()
			{
				for (int y = 0; y < _board.GetLength(1); y++)
				{
					for (int x = 0; x < _board.GetLength(0); x++)
					{
						AddToNumberOfPiecesInALine(x, y);
						if (CheckForWin()) return;
					}
					currentNumberOfPiecesInALine = 0;
				}
			}

			void LoopVertically()
			{
				for (int x = 0; x < _board.GetLength(0); x++)
				{
					for (int y = 0; y < _board.GetLength(1); y++)
					{
						AddToNumberOfPiecesInALine(x, y);
						if (CheckForWin()) return;
					}
					currentNumberOfPiecesInALine = 0;
				}
			}

			void LoopForwardDiagonally() // diagonally like a forward slash
			{
				
			}
			
			void LoopBackwardsDiagonally() // diagonally like a back slash
			{
				
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
