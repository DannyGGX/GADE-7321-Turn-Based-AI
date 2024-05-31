using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityUtils;


namespace DannyG
{
	
	public class BoardStateManager : Singleton<BoardStateManager>
	{
		public static int[,] grid => _boardState.grid;

		private static BoardState _boardState;
		public static BoardState boardState => _boardState;

		private void OnEnable()
		{
			_boardState = new BoardState
			{
				grid = SetupDataLocator.GameSetupData.startingGrid
			};
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
			_boardState.PlacePiece(moveData);
		}
		
		private void ShiftPieces(AllShiftedTilesData allShiftedTiles)
		{
			_boardState.ShiftPieces(allShiftedTiles);
		}

		public override string ToString()
		{
			return _boardState.ToString();
		}
	}
}
