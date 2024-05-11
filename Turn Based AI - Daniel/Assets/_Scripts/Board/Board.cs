using UnityEngine;


namespace DannyG
{
	/// <summary>
	/// Storing board state, and setting world positions of tiles
	/// </summary>
	public class Board<T>
	{
		private T[,] _grid;
		private int _width;
		private int _height;
		private readonly float _tileSize;
		private readonly Vector3 _originPosition;
		
		public Board(int width, int height, float tileSize, Vector3 originPosition)
		{
			_width = width;
			_height = height;
			_tileSize = tileSize;
			_originPosition = originPosition;
			_grid = new T[width, height];
		}
		public T[,] Grid
		{
			get => _grid;
			set => _grid = value;
		}
		
		public T GetGridObject(int x, int y)
		{
			return _grid[x, y];
		}
		
		
		public void SetGridObject(int x, int y, T obj)
		{
			_grid[x, y] = obj;
		}

		public Vector3[,] GetTileCenterWorldPositions()
		{
			Vector3[,] worldPositions = new Vector3[_width, _height];

			for (int y = 0; y < _width; y++)
			{
				for (int x = 0; x < _height; x++)
				{
					worldPositions[x, y] = GetTileCenterWorldPosition(x, y);
				}
			}
			return worldPositions;
		}
		public Vector3 GetTileCenterWorldPosition(int x, int y)
		{
			return new Vector3(x, y) * _tileSize + _originPosition;
		}
		
		private bool IsInBounds(int x, int y)
		{
			return x >= 0 && x < _width && y >= 0 && y < _height;
		}

		public void GetXY(Vector3 worldPosition, out int x, out int y)
		{
			x = Mathf.FloorToInt((worldPosition - _originPosition).x / _tileSize);
			y = Mathf.FloorToInt((worldPosition - _originPosition).y / _tileSize);
		}
		
		
		
	}
}
