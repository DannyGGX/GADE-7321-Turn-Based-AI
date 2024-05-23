using System.Collections.Generic;
using UnityEngine;


namespace DannyG
{
	
	public struct ShiftTilesLine
	{
		public List<Coordinate> lineOfTiles { get; private set; }
		public Incrementor2D shiftAmount { get; private set; }
		public int count => lineOfTiles.Count;
		
		public ShiftTilesLine(int constructorTrigger)
		{
			lineOfTiles = new List<Coordinate>();
			shiftAmount = new Incrementor2D();
		}

		public void AddTile(Coordinate currentPiecePosition, int gridValue)
		{
			lineOfTiles.Add(currentPiecePosition);
		}

		public void CreateShiftAmount(Coordinate firstPieceInLine, Coordinate landingPosition)
		{
			shiftAmount = (landingPosition - firstPieceInLine).ToIncrementor2D();
		}

		public bool IsNull()
		{
			return lineOfTiles.Count == 0;
		}

		public void SetToNull()
		{
			lineOfTiles.Clear();
		}
		
		public Coordinate ProvideAndRemoveLast()
		{
			Coordinate coordinate = lineOfTiles[^1];
			lineOfTiles.RemoveAt(lineOfTiles.Count - 1);
			return coordinate;
		}
	}
}
