using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace DannyG
{
	
	public class BoardPiecesUI : MonoBehaviour
	{
		private GameSetupDataSO _gameSetupData;
		private TileDisplayFactorySO _tileDisplayFactory;
		
		private Dictionary<Coordinate , Piece> _pieces = new Dictionary<Coordinate, Piece>();

		private void Start()
		{
			EventManager.onPlacePiece.Subscribe(CreatePiece);
			EventManager.onApplyGravityShiftToDisplay.Subscribe(ShiftPieces);
			
			_gameSetupData = SetupDataLocator.GameSetupData;
			_tileDisplayFactory = _gameSetupData.tileFactory;
		}


		private void OnDestroy()
		{
			EventManager.onPlacePiece.Unsubscribe(CreatePiece);
			EventManager.onApplyGravityShiftToDisplay.Unsubscribe(ShiftPieces);
		}
		
		private async void CreatePiece(MoveData moveData)
		{
			Piece currentPiece = _tileDisplayFactory.CreatePiece(
				MoveData.ConvertToTileType(moveData.PlayerId)
				, moveData.Coordinate.x
				, moveData.Coordinate.y
				, transform);
			
			_pieces.Add(moveData.Coordinate, currentPiece);

			MovePiece(currentPiece, moveData.Coordinate);
		}

		private void MovePiece(Piece piece, Coordinate coordinate)
		{
			Vector3 targetPosition = _gameSetupData.tileCenterPositions[coordinate.x, coordinate.y];
			piece.MoveTo(targetPosition, OnPieceFinishedMoving);
		}
		
		private void OnPieceFinishedMoving()
		{
			EventManager.onBoardDisplayFinishedUpdating.Invoke();
		}
		
		private void ShiftPieces(AllShiftedTilesData allShiftedTiles)
		{
			
		}
	}
}
