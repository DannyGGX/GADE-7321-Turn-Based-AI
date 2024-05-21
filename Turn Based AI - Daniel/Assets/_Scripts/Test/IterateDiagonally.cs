using System;
using UnityEngine;


namespace DannyG
{
	
	public class IterateDiagonally : MonoBehaviour
	{
		private char[,] _board =
		{
			{'A', 'B', 'C', 'D', 'E'},
			{'F', 'G', 'H', 'I', 'J'},
			{'K', 'L', 'M', 'N', 'O'},
			{'P', 'Q', 'R', 'S', 'T'},
			{'U', 'V', 'W', 'X', 'Y'}
		};

		private char[,] _outputBoard = new char[5, 10];
		
		private void Start()
		{
			Algorithm3();
			PrintOutput();
		}

		private void Algorithm1()
		{
			int i = 0;
			int outputX = 0;
			int outputY = 0;
			int xLength = _board.GetLength(0);
			for (int x = 0; x < _board.GetLength(0); x++)
			{
				outputX = 0;
				for (int y = 0; y <= x; y++)
				{
					i = x - y;
					_outputBoard[outputX, outputY] = _board[i, y];
					outputX++;
				}
				outputY++;
			}
			
			for (int k = _board.GetLength(0) - 2; k >= 0; k--)
			{
				outputX = 0;
				for (int j = 0; j <= k; j++)
				{
					i = k - j;
					_outputBoard[outputX, outputY] = _board[xLength - j - 1, xLength - i - 1];
					outputX++;
				}
				outputY++;
			}
		}

		private void Algorithm2()
		{
			int outputX = 0;
			int outputY = 0;
			const int cutoff = 3; // for avoiding counting diagonals that have 3 elements or less.
			for (int x = cutoff; x < _board.GetLength(0); x++)
			{
				outputX = 0;
				for (int y = 0; y <= x; y++)
				{
					_outputBoard[outputX, outputY] = _board[x - y, y];
					outputX++;
				}
				outputY++;
				
			}
			
			for (int k = _board.GetLength(0) - 2; k >= cutoff; k--)
			{
				outputX = 0;
				for (int j = 0; j <= k; j++)
				{
					int i = k - j;
					_outputBoard[outputX, outputY] = _board[_board.GetLength(0) - j - 1, _board.GetLength(0) - i - 1];
					outputX++;
				}
				outputY++;
			}
			
		}

		private void Algorithm3()
		{
			int xLength = _board.GetLength(0);
			int yLength = _board.GetLength(1);
			int outputX = 0;
			int outputY = 0;
			const int cutOff = 3;

			for (int y = cutOff; y < yLength; y++)
			{
				int row = y;
				int col = 0;
				outputX = 0;
				while (row >= 0)
				{
					_outputBoard[outputX, outputY] = _board[row, col];
					outputX++;
					row--;
					col++;
				}
				outputY++;
			}

			for (int x = 1; x < xLength - cutOff; x++)
			{
				int row = yLength - 1;
				int col = x;
				outputX = 0;
				while (col < xLength)
				{
					_outputBoard[outputX, outputY] = _board[row, col];
					outputX++;
					row--;
					col++;
				}
				outputY++;
			}
			
		}

		private void BackSlashDiagonalAlgorithm1()
		{
			int xLength = _board.GetLength(0);
			int yLength = _board.GetLength(1);
			int outputX = 0;
			int outputY = 0;

			for (int y = 3; y < yLength; y++)
			{
				int row = y;
				int col = xLength - 1;
				outputX = 0;
				while (row >= 0)
				{
					_outputBoard[outputX, outputY] = _board[row, col];
					outputX++;
					row--;
					col--;
				}
				outputY++;
			}

			for (int x = xLength - 2; x >= 3; x--)
			{
				int row = yLength - 1;
				int col = x;
				outputX = 0;
				while (col >= 0)
				{
					_outputBoard[outputX, outputY] = _board[row, col];
					outputX++;
					row--;
					col--;
				}
				outputY++;
			}
		}

		private void PrintOutput()
		{
			for (int y = 0; y < _outputBoard.GetLength(1); y++)
			{
				var line = "";
				for (int x = 0; x < _outputBoard.GetLength(0); x++)
				{
					line += $"{_outputBoard[x, y]} ";
				}
				Debug.Log(line);
			}
		}
	}
}
