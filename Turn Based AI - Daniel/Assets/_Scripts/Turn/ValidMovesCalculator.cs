using System;
using System.Collections.Generic;
using UnityEngine;


namespace DannyG
{

	public class ValidMovesCalculator
	{

		public List<Coordinate> GetValidMoves()
		{
			List<Coordinate> validMoves;
			
			switch (GravityManager.currentGravityState)
			{
				case GravityStates.Down:
					validMoves = CalculateValidMoves(false, true);
					//ForEachColumnOrderedTopToBottom(out validMoves); // older implementation. It worked, but wasn't adaptable.
					break;
				case GravityStates.Right:
					validMoves = CalculateValidMoves(true, false);
					break;
				case GravityStates.Up:
					validMoves = CalculateValidMoves(false, false);
					break;
				case GravityStates.Left:
					validMoves = CalculateValidMoves(true, true);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return validMoves;
		}

		private void ForEachColumnOrderedTopToBottom(out List<Coordinate> validMoves)
		{
			int[,] grid = BoardStateManager.Instance.grid;

			validMoves = new List<Coordinate>();
			var currentValidMove = new Coordinate();

			for (int x = 0; x < grid.GetLength(0); x++)
			{
				int y = grid.GetLength(1) - 1;
				// if first cell is a blocker	// skip to last blocker in a line
				while (y > 0 && grid[x, y] != (int)TileType.Empty)
				{
					y--;
				}
				if (y == 0) continue; // if there are no empty tiles
				
				// look for last empty tile in a line
				while (y >= 0 && grid[x, y] == (int)TileType.Empty)
				{
					currentValidMove.x = x;
					currentValidMove.y = y;
					y--;
				}
				validMoves.Add(currentValidMove);
			}
		}

		private List<Coordinate> CalculateValidMoves(bool inXDimension, bool inReverseLoop)
		{
			var grid = BoardStateManager.Instance.grid;
			var validMoves = new List<Coordinate>();
			var currentValidMove = new Coordinate();
			
			var targetDimension = inXDimension ? 0 : 1;
			var targetDimensionLength = inXDimension ? grid.GetLength(0) : grid.GetLength(1);
			var otherDimensionLength = inXDimension ? grid.GetLength(1) : grid.GetLength(0);
			
			int incrementor = inReverseLoop ? -1 : 1;
			int targetDimensionStart = inReverseLoop ? targetDimensionLength - 1 : 0;
			int targetDimensionEnd = inReverseLoop ? 0 : targetDimensionLength;
			
			
			for (int otherDimensionIndex = 0; otherDimensionIndex < otherDimensionLength; otherDimensionIndex++)
			{
				int targetDimensionIndex = targetDimensionStart;
				
				if (inReverseLoop) // the only difference in these if else statements is the check for end of loops
				{
					// if first cell is a blocker	// skip to last blocker in a line
					while (targetDimensionIndex > targetDimensionEnd && grid[otherDimensionIndex, targetDimensionIndex] != (int)TileType.Empty)
					{
						targetDimensionIndex += incrementor;
					}
					if (targetDimensionIndex == targetDimensionEnd) continue; // if there are no empty tiles
				
					// look for last empty tile in a line
					while (targetDimensionIndex >= targetDimensionEnd && grid[otherDimensionIndex, targetDimensionIndex] == (int)TileType.Empty)
					{
						if (targetDimension == targetDimensionEnd)
						{
							currentValidMove.x = targetDimensionIndex;
							currentValidMove.y = otherDimensionIndex;
						}
						else
						{
							currentValidMove.x = otherDimensionIndex;
							currentValidMove.y = targetDimensionIndex;
						}

						targetDimensionIndex += incrementor;
					}
				}
				else
				{
					// if first cell is a blocker	// skip to last blocker in a line
					while (targetDimensionIndex < targetDimensionEnd && grid[otherDimensionIndex, targetDimensionIndex] != (int)TileType.Empty)
					{
						targetDimensionIndex += incrementor;
					}
					if (targetDimensionIndex == targetDimensionEnd) continue; // if there are no empty tiles
				
					// look for last empty tile in a line
					while (targetDimensionIndex <= targetDimensionEnd && grid[otherDimensionIndex, targetDimensionIndex] == (int)TileType.Empty)
					{
						if (targetDimension == targetDimensionEnd)
						{
							currentValidMove.x = targetDimensionIndex;
							currentValidMove.y = otherDimensionIndex;
						}
						else
						{
							currentValidMove.x = otherDimensionIndex;
							currentValidMove.y = targetDimensionIndex;
						}

						targetDimensionIndex += incrementor;
					}
				}
				validMoves.Add(currentValidMove);
			}
			return validMoves;
		}
		
	}
}
