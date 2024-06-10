using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace DannyG
{
	/// <summary>
	/// Used to find the amount of pieces in a line in a given board state.
	///
	/// The win checker and the utility function of the minimax algorithm use this class.
	/// </summary>
	public static class LineOfPiecesOperations
	{
		/// <summary>
		/// Used to find coordinates of a certain tile type around a center coordinate.
		/// </summary>
		/// <param name="centerCoordinate"> The placed piece from which to get the neighboring coordinates </param>
		/// <param name="boardState">  </param>
		/// <param name="targetTileType"> Only coordinates with this tile type will be returned.
		/// If null, then it will be set to the tile type of the center coordinate. </param>
		/// <returns> List of neighboring coordinates </returns>
		public static List<Coordinate> GetNeighboringCoordinates(Coordinate centerCoordinate, BoardState boardState, TileType? targetTileType = null)
		{
			// if targetTileType is null, then it will be set to the tile type of the center coordinate
			targetTileType ??= boardState.GetTileTypeAt(centerCoordinate);
			HashSet<Coordinate> possibleCoordinates = GetPossibleCoordinatesAroundCenterCoordinate();
			var resultCoordinates = new List<Coordinate>();
			
			AddTargetCoordinates();
			RemoveOppositeCoordinates();
			
			return resultCoordinates;

			HashSet<Coordinate> GetPossibleCoordinatesAroundCenterCoordinate()
			{
				var result = new HashSet<Coordinate>
				{
					centerCoordinate.WithUpdatedValues(x: 1),
					centerCoordinate.WithUpdatedValues(x: -1),
					centerCoordinate.WithUpdatedValues(y: 1),
					centerCoordinate.WithUpdatedValues(y: -1),
					centerCoordinate.WithUpdatedValues(1, 1),
					centerCoordinate.WithUpdatedValues(1, -1),
					centerCoordinate.WithUpdatedValues(-1, 1),
					centerCoordinate.WithUpdatedValues(-1, -1)
				};
				boardState.RemoveAllThatAreOutOfBounds(ref result);
				return result;
			}

			void AddTargetCoordinates()
			{
				resultCoordinates.AddRange
				(possibleCoordinates.Where
					(
						coordinate => boardState.GetTileTypeAt(coordinate) == targetTileType
					)
				);
			}

			void RemoveOppositeCoordinates()
			{
				resultCoordinates = resultCoordinates
					.Where
					(
						coordinate => coordinate != GetOppositeCenterNeighbor(coordinate)
					)
					.ToList();
				
				return;
				
				Coordinate GetOppositeCenterNeighbor(Coordinate coordinate1)
				{
					var result = new Coordinate(centerCoordinate);
					Incrementor2D incrementor = new Incrementor2D(centerCoordinate, coordinate1);
					result.Increment(incrementor);
					return result;
				}
			}
		}

		/// <summary>
		/// Used to find the amount of tiles of a particular type in a line starting from a center coordinate.
		/// The opposite direction is also checked.
		/// </summary>
		/// <param name="centerCoordinate"> The starting coordinate </param>
		/// <param name="nextCoordinate"> The coordinate next to the center coordinate.
		/// The incrementor is determined by the difference between the 2 given coordinates. </param>
		/// <param name="boardState"></param>
		/// <returns> Number of same tiles in a line starting from center coordinate in the direction of the next coordinate
		/// as well as the opposite direction.
		/// Returns values 2 to 4 </returns>
		public static int GetNumberOfTilesInALine(Coordinate centerCoordinate, Coordinate nextCoordinate, 
			BoardState boardState)
		{
			// The target tile type is set to the tile type of the next coordinate.
			// This can be used for checking the amount of empty tiles around a placed piece.
			TileType targetTileType = boardState.GetTileTypeAt(nextCoordinate);
			Incrementor2D incrementor = new Incrementor2D(centerCoordinate, nextCoordinate);
			int numberOfSameTiles = 2;
			Coordinate currentCoordinate = nextCoordinate;

			CountSameTiles();
			
			//Check opposite direction
            incrementor.SetOpposite();
            currentCoordinate = centerCoordinate;
			CountSameTiles();
            
			return numberOfSameTiles;

			void CountSameTiles()
			{
				bool continueLoop = true;
				while (continueLoop)
				{
					currentCoordinate.Increment(incrementor);

					if (boardState.IsInBounds(currentCoordinate) == false)
					{
						return;
					}
				
					TileType currentTile = boardState.GetTileTypeAt(currentCoordinate);
					if (currentTile == targetTileType)
					{
						numberOfSameTiles++;
						continueLoop = numberOfSameTiles < 4; // exit the loop if enough of the same tiles are found. 4 is enough
					}
					else return;
				}
			}
		}

		/// <summary>
		/// Find the longest line of tiles around a center coordinate.
		/// The target tile type is the same as the center coordinate.
		/// </summary>
		/// <param name="centerCoordinate"></param>
		/// <param name="boardState"></param>
		/// <returns> The number of coordinates in the longest line. Returns values 1 to 4. </returns>
		public static int GetLongestLineOfTilesInArea(Coordinate centerCoordinate, BoardState boardState)
		{
			var result = 1;
			const int cutOff = 4;
			List<Coordinate> neighboringCoordinates = GetNeighboringCoordinates(centerCoordinate, boardState);
			if(neighboringCoordinates.Count == 0)
			{
				return result;
			}

			foreach (var coordinate in neighboringCoordinates)
			{
				int currentLineCount = GetNumberOfTilesInALine(centerCoordinate, coordinate, boardState);
				if (currentLineCount >= cutOff)
				{
					return currentLineCount;
				}
				if (currentLineCount > result)
				{
					result = currentLineCount;
				}
			}
			return result;
		}
		
	}
}
