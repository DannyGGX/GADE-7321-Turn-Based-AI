using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace DannyG
{
	
	public static class LineOfPiecesOperations
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="centerCoordinate"></param>
		/// <param name="boardState"></param>
		/// <returns> List of coordinates of pieces with the same tile type as the center coordinate </returns>
		public static List<Coordinate> GetNeighboringCoordinatesOfSameTileType(Coordinate centerCoordinate, BoardState boardState)
		{
			TileType targetTileType = boardState.GetTileTypeAt(centerCoordinate);
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

				// Remove coordinates that are out of bounds
				foreach (var coordinate in result.Where(coordinate => boardState.IsInBounds(coordinate) == false))
				{
					result.Remove(coordinate);
				}
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
				foreach (var oppositeCoordinate in resultCoordinates.Select(GetOppositeCenterNeighbor))
				{
					if (resultCoordinates.Contains(oppositeCoordinate))
					{
						resultCoordinates.Remove(oppositeCoordinate);
					}
				}
				return;
				
				Coordinate GetOppositeCenterNeighbor(Coordinate coordinate1)
				{
					var result = new Coordinate(centerCoordinate);
					Incrementor2D incrementor = new Incrementor2D(centerCoordinate, coordinate1);
					incrementor.SetOpposite();
					result.Increment(incrementor);
					return result;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="centerCoordinate"> The starting coordinate </param>
		/// <param name="nextCoordinate"> The coordinate next to the center coordinate </param>
		/// <param name="boardState"></param>
		/// <returns> Number of tiles with the same tile type as the center coordinate. returns values 2 to 4 </returns>
		public static int GetNumberOfSameTilesInALine(Coordinate centerCoordinate, Coordinate nextCoordinate, 
			BoardState boardState)
		{
			TileType targetTileType = boardState.GetTileTypeAt(centerCoordinate);
			Incrementor2D incrementor = new Incrementor2D(centerCoordinate, nextCoordinate);
			int numberOfSameTiles = 1;
			Coordinate currentCoordinate = centerCoordinate;

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
						continueLoop = numberOfSameTiles < 4;
					}
					else
					{
						return;
					}
				}
			}
		}

		public static int GetLongestLineOfTilesInArea(Coordinate centerCoordinate, BoardState boardState)
		{
			var result = 1;
			const int cutOff = 4;
			List<Coordinate> neighboringCoordinates = GetNeighboringCoordinatesOfSameTileType(centerCoordinate, boardState);
			if(neighboringCoordinates.Count == 0)
			{
				return result;
			}

			foreach (var coordinate in neighboringCoordinates)
			{
				int currentLineCount = GetNumberOfSameTilesInALine(centerCoordinate, coordinate, boardState);
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
