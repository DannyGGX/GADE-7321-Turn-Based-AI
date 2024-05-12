using System;
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
		}

		private void OnDisable()
		{
			EventManager.onPlacePiece.Unsubscribe(PlacePiece);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="inReverseOrder"> true if from top to bottom </param>
		/// <returns></returns>
		public int[] GetLengthOfCellsInColumn(int x, bool inReverseOrder = false)
		{
			int[] result = new int[grid.GetLength(1)];
			if (inReverseOrder)
			{
				for (int i = grid.GetLength(1) - 1; i >= 0; i--)
				{
					result[i] = grid[x, i];
				}
			} else
			{
				for (int i = 0; i < grid.GetLength(1); i++)
				{
					result[i] = grid[x, i];
				}
			}
			return result;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="y"></param>
		/// <param name="inReverseOrder"> true if from right to left </param>
		/// <returns></returns>
		public int[] GetLengthOfCellsInRow(int y, bool inReverseOrder = false)
		{
			int[] result = new int[grid.GetLength(0)];
			if (inReverseOrder)
			{
				for (int i = grid.GetLength(0) - 1; i >= 0; i--)
				{
					result[i] = grid[i, y];
				}
			} else
			{
				for (int i = 0; i < grid.GetLength(0); i++)
				{
					result[i] = grid[i, y];
				}
			}
			return result;
		}
		
		private void PlacePiece(MoveData moveData)
		{
			grid[moveData.Coordinate.x, moveData.Coordinate.y] = (int)moveData.PlayerId;
		}
	}
}
