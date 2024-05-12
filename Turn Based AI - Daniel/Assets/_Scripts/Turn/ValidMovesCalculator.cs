using System;
using System.Collections.Generic;
using UnityEngine;


namespace DannyG
{

	public class ValidMovesCalculator
	{

		public List<Coordinate> CalculateValidMoves()
		{
			List<Coordinate> validMoves;
			
			switch (GravityManager.currentGravityState)
			{
				case GravityStates.Down:
					ForEachColumnOrderedTopToBottom(out validMoves);
					break;
				case GravityStates.Right:
					ForEachRowOrderedLeftToRight(out validMoves);
					break;
				case GravityStates.Up:
					ForEachColumnOrderedBottomToTop(out validMoves);
					break;
				case GravityStates.Left:
					ForEachRowOrderedRightToLeft(out validMoves);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return validMoves;
		}

		private void ForEachColumnOrderedTopToBottom(out List<Coordinate> validMoves)
		{
			int[,] grid = BoardStateManager.Instance.grid;

			bool noMoreBlockersBlockingEdge = false;
			Coordinate validMove = new Coordinate();
			validMoves = new List<Coordinate>();

			for (int x = 0; x < grid.GetLength(0); x++)
			{
				for (int y = grid.GetLength(1) - 1; y >= 0; y--)
				{
					if (grid[x, y] == (int)TileType.Empty || noMoreBlockersBlockingEdge)
					{
						noMoreBlockersBlockingEdge = true;

						if (grid[x, y] == (int)TileType.Empty)
						{
							validMove.x = x;
							validMove.y = y;
						}
						else if (grid[x, y] != (int)TileType.Empty && validMove.IsNull() == false)
						{
							validMoves.Add(validMove);
							break;
						}
					}
				}
			}
		}

		private void ForEachRowOrderedLeftToRight(out List<Coordinate> validMoves)
		{
			int[,] grid = BoardStateManager.Instance.grid;

			validMoves = new List<Coordinate>();

			for (int y = 0; y < grid.GetLength(1); y++)
			{
				bool noMoreBlockersBlockingEdge = false;
				Coordinate validMove = new Coordinate();
				
				for (int x = 0; x < grid.GetLength(0); x++)
				{
					if (grid[x, y] == (int)TileType.Empty || noMoreBlockersBlockingEdge)
					{
						noMoreBlockersBlockingEdge = true;

						if (grid[x, y] == (int)TileType.Empty)
						{
							validMove.x = x;
							validMove.y = y;
						}
						else if (grid[x, y] != (int)TileType.Empty && validMove.IsNull() == false)
						{
							validMoves.Add(validMove);
							break;
						}
					}
				}
			}
		}

		private void ForEachColumnOrderedBottomToTop(out List<Coordinate> validMoves)
		{
			int[,] grid = BoardStateManager.Instance.grid;

			validMoves = new List<Coordinate>();

			for (int x = 0; x < grid.GetLength(0); x++)
			{
				bool noMoreBlockersBlockingEdge = false;
				Coordinate validMove = new Coordinate();

				for (int y = 0; y < grid.GetLength(1); y++)
				{
					if (grid[x, y] == (int)TileType.Empty || noMoreBlockersBlockingEdge)
					{
						noMoreBlockersBlockingEdge = true;

						if (grid[x, y] == (int)TileType.Empty)
						{
							validMove.x = x;
							validMove.y = y;
						}
						else if (grid[x, y] != (int)TileType.Empty && validMove.IsNull() == false)
						{
							validMoves.Add(validMove);
							break;
						}
					}
				}
			}

		}

		private void ForEachRowOrderedRightToLeft(out List<Coordinate> validMoves)
		{
			int[,] grid = BoardStateManager.Instance.grid;

			validMoves = new List<Coordinate>();

			for (int y = 0; y < grid.GetLength(1); y++)
			{
				bool noMoreBlockersBlockingEdge = false;
				Coordinate validMove = new Coordinate();

				for (int x = grid.GetLength(0) - 1; x >= 0; x--)
				{
					if (grid[x, y] == (int)TileType.Empty || noMoreBlockersBlockingEdge)
					{
						noMoreBlockersBlockingEdge = true;

						if (grid[x, y] == (int)TileType.Empty)
						{
							validMove.x = x;
							validMove.y = y;
						}
						else if (grid[x, y] != (int)TileType.Empty && validMove.IsNull() == false)
						{
							validMoves.Add(validMove);
							break;
						}
					}
				}
			}
		}
	}
}
