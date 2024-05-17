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
