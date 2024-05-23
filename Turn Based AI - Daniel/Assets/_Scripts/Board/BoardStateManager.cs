using System;
using UnityEngine;
using UnityUtils;


namespace DannyG
{
	
	public class BoardStateManager : Singleton<BoardStateManager>
	{
		public int[,] grid { get; private set; }

		private void OnEnable()
		{
			grid = SetupDataLocator.GameSetupData.startingGrid;
			
			EventManager.onPlacePiece.Subscribe(PlacePiece);
			EventManager.onApplyGravityShiftToDisplay.Subscribe(ShiftPieces);
		}

		private void OnDisable()
		{
			EventManager.onPlacePiece.Unsubscribe(PlacePiece);
			EventManager.onApplyGravityShiftToDisplay.Unsubscribe(ShiftPieces);
		}

		private void PlacePiece(MoveData moveData)
		{
			grid[moveData.Coordinate.x, moveData.Coordinate.y] = (int)moveData.PlayerId;
		}
		
		private void ShiftPieces(AllShiftedTilesData allShiftedTiles)
		{
			
		}
	}
}
