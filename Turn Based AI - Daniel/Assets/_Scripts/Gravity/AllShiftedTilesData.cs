using System.Collections.Generic;
using UnityEngine;


namespace DannyG
{
	
	public struct AllShiftedTilesData
	{
		public List<ShiftTilesLine> listOfShiftedTiles { get; private set; }

		public AllShiftedTilesData(int count = 10)
		{
			listOfShiftedTiles = new List<ShiftTilesLine>(count);
		}

		public void AddLineAndSetToNull(ShiftTilesLine currentLine)
		{
			listOfShiftedTiles.Add(currentLine);
			currentLine.SetToNull();
		}
	}
}
