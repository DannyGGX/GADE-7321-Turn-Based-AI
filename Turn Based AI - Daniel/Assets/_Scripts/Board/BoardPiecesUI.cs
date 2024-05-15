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
		
		private void OnEnable()
		{
			EventManager.onPlacePiece.Subscribe(CreatePiece);
			EventManager.onApplyGravityShiftToDisplay.Subscribe(ShiftPieces);

			
		}

		private void Start()
		{
			_gameSetupData = SetupDataLocator.GameSetupData;
			_tileDisplayFactory = _gameSetupData.tileFactory;
		}


		private void OnDisable()
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

			PlacePiece(currentPiece, moveData.Coordinate);
			await Task.Yield();
			EventManager.onBoardDisplayFinishedUpdating.Invoke(); // temporary
		}

		private void PlacePiece(Piece piece, Coordinate coordinate)
		{
			Vector3 startingPosition = GetStartingPosition(coordinate.x, coordinate.y);
			Vector3 targetPosition = _gameSetupData.tileCenterPositions[coordinate.x, coordinate.y];
			piece.Place(startingPosition, targetPosition, _gameSetupData.overallScaleModifier);
		}

		private Vector3 GetStartingPosition(int x, int y)
		{
			GravityStates currentGravityState = GravityManager.currentGravityState;
			Vector3[] startingPositions = _gameSetupData.startingPositions[currentGravityState];
			switch (currentGravityState)
			{
				case GravityStates.Down:
				case GravityStates.Up:
					return startingPositions[x];
				case GravityStates.Left:
				case GravityStates.Right:
					return startingPositions[y];
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void ShiftPieces(AllShiftedTilesData allShiftedTiles)
		{
			
		}
	}
}
