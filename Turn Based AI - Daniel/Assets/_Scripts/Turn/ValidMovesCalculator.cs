using System;
using System.Collections.Generic;
using UnityEngine;


namespace DannyG
{

	public class ValidMovesCalculator
	{
		private int[,] _grid;
		
		// dimension variables
		private int _targetDimensionLength;
		private int _otherDimensionLength;
		private Action _setCurrentValidMove;
		private delegate int GetCurrentGridValue();
		private GetCurrentGridValue _getCurrentGridValue;
		
		// loop direction variables
		private int _incrementor;
		private int _reachEndLoopAddition;
		private int _targetDimensionStartIndex;
		private int _targetDimensionEndIndex;
		private delegate bool LoopEndCondition(int index);
		private LoopEndCondition _endCondition;

		public List<Coordinate> GetValidMoves(BoardState? boardState = null)
		{
			_grid = boardState?.grid ?? BoardStateManager.grid;
			
			return GravityManager.currentGravityState switch
			{
				GravityStates.Down => CalculateValidMoves(false, true),
				//GravityStates.Down => ForEachColumnOrderedTopToBottom(); // older implementation. It worked, but wasn't adaptable.
				GravityStates.Right => CalculateValidMoves(true, false),
				GravityStates.Up => CalculateValidMoves(false, false),
				GravityStates.Left => CalculateValidMoves(true, true),
				_ => throw new ArgumentOutOfRangeException()
			};
		}

		/// <summary>
		/// This is the more basic, but less repeatable version of the below function.
		/// This one can be used to more easily understand the below function.
		/// </summary>
		/// <param name="validMoves"></param>
		private List<Coordinate> ForEachColumnOrderedTopToBottom()
		{
			int[,] grid = BoardStateManager.grid;

			var validMoves = new List<Coordinate>();
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
			return validMoves;
		}
		
		
		private List<Coordinate> CalculateValidMoves(bool inXDimension, bool inReverseLoop)
		{
			var validMoves = new List<Coordinate>();
			var currentValidMove = new Coordinate();

			// set current loop index variables before the Action delegate that uses them.
			int otherDimensionIndex = default;
			int targetDimensionIndex = default;
			
			SetDimensionVariables(inXDimension);
			SetLoopDirectionVariables(inReverseLoop);

			// loop normally through the non-target dimension
			for (otherDimensionIndex = 0; otherDimensionIndex < _otherDimensionLength; otherDimensionIndex++)
			{
				targetDimensionIndex = _targetDimensionStartIndex;
				// if first cell is not empty, loop until an empty cell is found
				while (_endCondition(targetDimensionIndex) && _getCurrentGridValue() != (int)TileType.Empty)
				{
					targetDimensionIndex += _incrementor;
				}
				
				// if there are no empty tiles in the row or column
				if (targetDimensionIndex == _targetDimensionEndIndex) continue; 
			
				// once the first empty tile is found, loop until the current cell is not an empty tile
				while (_endCondition(targetDimensionIndex + _reachEndLoopAddition) && _getCurrentGridValue() == (int)TileType.Empty)
					                                        //^ +1 or -1 so that operator in endCondition can work like >= or <=
				{
					targetDimensionIndex += _incrementor;
				}
				
				targetDimensionIndex += _incrementor * -1; // go back to index before, then save the valid move
				_setCurrentValidMove.Invoke();
				validMoves.Add(currentValidMove);
			}
			return validMoves;
			
			void SetDimensionVariables(bool inXDimension)
			{
				if (inXDimension)
				{
					_targetDimensionLength = _grid.GetLength(0);
					_otherDimensionLength = _grid.GetLength(1);
					_setCurrentValidMove = SetCurrentValidMoveForXTargetDimension;
					_getCurrentGridValue = GetGridValueForXTargetDimension;
				}
				else
				{
					_targetDimensionLength = _grid.GetLength(1);
					_otherDimensionLength = _grid.GetLength(0);
					_setCurrentValidMove = SetCurrentValidMoveForYTargetDimension;
					_getCurrentGridValue = GetGridValueForYTargetDimension;
				}
			}
			void SetLoopDirectionVariables(bool inReverseLoop)
			{
				if (inReverseLoop)
				{
					_incrementor = -1;
					_reachEndLoopAddition = 1;
					_targetDimensionStartIndex = _targetDimensionLength - 1;
					_targetDimensionEndIndex = 0;
					_endCondition = ReverseLoopEndCondition;
				}
				else
				{
					_incrementor = 1;
					_reachEndLoopAddition = -1;
					_targetDimensionStartIndex = 0;
					_targetDimensionEndIndex = _targetDimensionLength - 1;
					_endCondition = ForwardLoopEndCondition;
				}
			}

			bool ForwardLoopEndCondition(int index) => index < _targetDimensionEndIndex;
			bool ReverseLoopEndCondition(int index) => index > _targetDimensionEndIndex;

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
			
			int GetGridValueForXTargetDimension() => _grid[targetDimensionIndex, otherDimensionIndex];
			int GetGridValueForYTargetDimension() => _grid[otherDimensionIndex, targetDimensionIndex];
			
		}
		
	}
}
