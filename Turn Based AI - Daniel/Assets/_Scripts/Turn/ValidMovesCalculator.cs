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

		/// <summary>
		/// This is the more basic, but less repeatable version of the below function.
		/// This one can be used to more easily understand the below function.
		/// </summary>
		/// <param name="validMoves"></param>
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

		private delegate bool LoopEndCondition(int index);
		private delegate int GetCurrentGridValue();
		
		private List<Coordinate> CalculateValidMoves(bool inXDimension, bool inReverseLoop)
		{
			var grid = BoardStateManager.Instance.grid;
			var validMoves = new List<Coordinate>();
			var currentValidMove = new Coordinate();

			// set current loop index variables before the Action delegate that uses them.
			int otherDimensionIndex = default;
			int targetDimensionIndex = default;
			
			// set dimension variables
			var targetDimensionLength = inXDimension ? grid.GetLength(0) : grid.GetLength(1);
			var otherDimensionLength = inXDimension ? grid.GetLength(1) : grid.GetLength(0);
			Action setCurrentValidMove = inXDimension ? SetCurrentValidMoveForXTargetDimension : SetCurrentValidMoveForYTargetDimension;
			GetCurrentGridValue getCurrentGridValue =
				inXDimension ? GetGridValueForXTargetDimension : GetGridValueForYTargetDimension;
			
			// set loop direction variables
			int incrementor = inReverseLoop ? -1 : 1;
			int reachEndLoopAddition = inReverseLoop ? 1 : -1;
			int targetDimensionStartIndex = inReverseLoop ? targetDimensionLength - 1 : 0;
			int targetDimensionEndIndex = inReverseLoop ? 0 : targetDimensionLength - 1;
			LoopEndCondition endCondition = inReverseLoop ? ReverseLoopEndCondition : ForwardLoopEndCondition;

			// loop normally through the non-target dimension
			for (otherDimensionIndex = 0; otherDimensionIndex < otherDimensionLength; otherDimensionIndex++)
			{
				targetDimensionIndex = targetDimensionStartIndex;
				// if first cell is not empty, loop until an empty cell is found
				while (endCondition(targetDimensionIndex) && getCurrentGridValue() != (int)TileType.Empty)
				{
					targetDimensionIndex += incrementor;
				}
				
				// if there are no empty tiles in the row or column
				if (targetDimensionIndex == targetDimensionEndIndex) continue; 
			
				// once the first empty tile is found, loop until the current cell is not an empty tile
				while (endCondition(targetDimensionIndex + reachEndLoopAddition) && getCurrentGridValue() == (int)TileType.Empty)
					                                        //^ +1 or -1 so that operator in endCondition can work like >= or <=
				{
					targetDimensionIndex += incrementor;
				}
				
				targetDimensionIndex += incrementor * -1; // go back to index before, then save the valid move
				setCurrentValidMove.Invoke();
				validMoves.Add(currentValidMove);
			}
			return validMoves;

			bool ForwardLoopEndCondition(int index) => index < targetDimensionEndIndex;
			bool ReverseLoopEndCondition(int index) => index > targetDimensionEndIndex;

			void SetCurrentValidMoveForXTargetDimension()
			{
				currentValidMove.x = targetDimensionIndex;
				currentValidMove.y = otherDimensionIndex;
			}
			void SetCurrentValidMoveForYTargetDimension()
			{
				currentValidMove.x = otherDimensionIndex;
				currentValidMove.y = targetDimensionIndex;
			}
			
			int GetGridValueForXTargetDimension() => grid[targetDimensionIndex, otherDimensionIndex];
			int GetGridValueForYTargetDimension() => grid[otherDimensionIndex, targetDimensionIndex];
		}
		
	}
}
