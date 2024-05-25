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
		
		private async void ShiftPieces(AllShiftedTilesData allShiftedTiles)
		{
			await Task.Yield(); // wait for board state to update before removing furthest piece
			if (allShiftedTiles.listOfShiftedTiles.Count == 0) // check if no pieces are to be shifted
			{
				await Task.Yield(); // wait for win checker to check for win
				OnPieceFinishedMoving();
				return;
			}
			
			Piece piece;
			Coordinate furthestCoordinate = allShiftedTiles.GetPieceWithLongestTravelAndRemoveItFromList(out var furthestShiftAmount);
			
			foreach (var line in allShiftedTiles.listOfShiftedTiles)
			{
				foreach (var tile in line.lineOfTiles)
				{
					piece = ChangePieceCoordinate(tile, line.shiftAmount);
					MovePiece(piece, piece.coordinate, DoNothing);
				}
			}
			
			// Move furthest piece afterwards to avoid having duplicate keys in the dictionary.
			// Duplicate keys exception happened when the furthest piece lands on another piece
			// before that piece has moved in the current gravity shift.
			piece = ChangePieceCoordinate(furthestCoordinate, furthestShiftAmount);
			MovePiece(piece, piece.coordinate, OnPieceFinishedMoving);
			
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
