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
		
		private void CreatePiece(MoveData moveData)
		{
			Piece currentPiece = _tileDisplayFactory.CreatePiece(
				MoveData.ConvertToTileType(moveData.PlayerId)
				, moveData.Coordinate.x
				, moveData.Coordinate.y
				, transform);
			
			_pieces.Add(moveData.Coordinate, currentPiece);

			MovePiece(currentPiece, moveData.Coordinate, OnPieceFinishedMoving);
		}

		private void MovePiece(Piece piece, Coordinate coordinate, Action onCompleteCallback)
		{
			Vector3 targetPosition = _gameSetupData.tileCenterPositions[coordinate.x, coordinate.y];
			piece.MoveTo(targetPosition, onCompleteCallback);
		}
		
		private static void OnPieceFinishedMoving()
		{
			EventManager.onBoardDisplayFinishedUpdating.Invoke();
		}

		private static void DoNothing()
		{
		}
		
		private void ShiftPieces(AllShiftedTilesData allShiftedTiles)
		{
			// the below is commented for debugging DOTween error
			
			Coordinate coordinate = allShiftedTiles.GetPieceWithLongestTravelAndRemoveItFromList(out var shiftAmount);
			Piece piece = ChangePieceCoordinate(coordinate, shiftAmount);
			MovePiece(piece, piece.coordinate, OnPieceFinishedMoving);
			
			foreach (var line in allShiftedTiles.listOfShiftedTiles)
			{
				foreach (var tile in line.lineOfTiles)
				{
					piece = ChangePieceCoordinate(tile, line.shiftAmount);
					MovePiece(piece, piece.coordinate, DoNothing);
				}
			}
			return;
			
			Piece ChangePieceCoordinate(Coordinate coordinate, Incrementor2D shiftAmount)
			{
				Piece currentPiece = _pieces[coordinate];
				_pieces.Remove(coordinate);
				coordinate.Increment(shiftAmount);
				_pieces.Add(coordinate, currentPiece);
				currentPiece.SetCoordinate(coordinate);
				return currentPiece;
			}
		}
	}
}
