using System.Collections.Generic;
using UnityEngine;


namespace DannyG
{
	
	public struct AllShiftedTilesData
	{
		public List<ShiftTilesLine> listOfShiftedTiles { get; private set; }

		public AllShiftedTilesData(int constructorTrigger)
		{
			listOfShiftedTiles = new List<ShiftTilesLine>();
		}

		public void AddLine(ShiftTilesLine currentLine)
		{
			listOfShiftedTiles.Add(currentLine);
		}
		
		public ShiftTilesLine GetLineWithLargestShiftAmount()
		{
			int lineWithLongestShiftIndex = 0;

			for (int index = 1; index < listOfShiftedTiles.Count; index++)
			{
				if (listOfShiftedTiles[index].shiftAmount > listOfShiftedTiles[lineWithLongestShiftIndex].shiftAmount)
				{
					lineWithLongestShiftIndex = index;
				}
			}
			return listOfShiftedTiles[lineWithLongestShiftIndex];
		}

		public Coordinate GetPieceWithLongestTravelAndRemoveItFromList(out Incrementor2D shiftAmount)
		{
			int lineWithLongestTravelIndex = 0;
			int longestTravelStretchAmount = 0;
			for (int index = 1; index < listOfShiftedTiles.Count; index++)
			{
				int currentTravelStretchAmount = GetTravelStretch(listOfShiftedTiles[index]);
				if (currentTravelStretchAmount > longestTravelStretchAmount)
				{
					lineWithLongestTravelIndex = index;
					longestTravelStretchAmount = currentTravelStretchAmount;
				}
			}
			ShiftTilesLine targetLine = listOfShiftedTiles[lineWithLongestTravelIndex];
			shiftAmount = targetLine.shiftAmount;
			return targetLine.ProvideAndRemoveLast();
			
			int GetTravelStretch(ShiftTilesLine line)
			{
				return line.shiftAmount.GetMagnitude() + line.count;
			}
		}
	}
}
