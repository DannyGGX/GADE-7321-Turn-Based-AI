using System;
using System.Collections.Generic;
using System.Text;
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
			int[] currentLineOfTileData;
			
			foreach (var line in allShiftedTiles.listOfShiftedTiles)
			{
				currentLineOfTileData = new int[line.count];
				for (int index = 0; index < line.count; index++)
				{
					int x = line.lineOfTiles[index].x;
					int y = line.lineOfTiles[index].y;
					currentLineOfTileData[index] = grid[x, y];
					grid[x, y] = (int)TileType.Empty;
				}
				PlaceTiles(line);
			}

			void PlaceTiles(ShiftTilesLine line)
			{
				Incrementor2D incrementor = line.shiftAmount.Normalize();
				incrementor.SetOpposite();
				int x = line.lineOfTiles[0].x + line.shiftAmount.x;
				int y = line.lineOfTiles[0].y + line.shiftAmount.y;
				Coordinate currentCoordinate = new Coordinate(x, y);

				for (int index = 0; index < line.count; index++)
				{
					grid[currentCoordinate.x, currentCoordinate.y] = currentLineOfTileData[index];
					currentCoordinate.Increment(incrementor);
				}
			}
			
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\n");
			for (int y = grid.GetLength(1) - 1; y >= 0; y--)
			{
				for (int x = 0; x < grid.GetLength(0); x++)
				{
					stringBuilder.Append($"{grid[x, y]} ");
				}
				stringBuilder.Append("\n");
			}
			return stringBuilder.ToString();
		}

		
	}
}
