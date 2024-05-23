using System;
using System.Collections.Generic;
using UnityEngine;


namespace DannyG
{
	
	public class NormalizeIncrementor2D : MonoBehaviour
	{
		private List<Incrementor2D> testList;

		private void AddToTestList()
		{
			testList = new List<Incrementor2D>();
			testList.Add(new Incrementor2D(1, 1));
			testList.Add(new Incrementor2D(-1, 0));
			testList.Add(new Incrementor2D(4, -4));
			testList.Add(new Incrementor2D(-6, -2));
			testList.Add(new Incrementor2D(0, 0));
		}
		
		private void NormalizeTestList()
		{
			foreach (var incrementor in testList)
			{
				Incrementor2D result = incrementor.Normalize();
				Debug.Log($"X: {result.x} | Y: {result.y}");
			}
		}
		
		private void PrintResults()
		{
			foreach (var incrementor in testList)
			{
				
			}
		}

		private void Start()
		{
			//AddToTestList();
			//NormalizeTestList();
			//PrintResults();
			TestShiftTilesLineIsNull();
		}

		private void TestShiftTilesLineIsNull()
		{
			ShiftTilesLine shiftTilesLine = new ShiftTilesLine(default);
			shiftTilesLine.lineOfTiles.Add(new Coordinate(0, 0));
			shiftTilesLine.lineOfTiles.Add(new Coordinate(0, 1));
			shiftTilesLine.lineOfTiles.Add(new Coordinate(0, 2));
			shiftTilesLine.SetToNull();
			
			Debug.Log($"Is Shift Tiles Line Null: {shiftTilesLine.IsNull()}");
		}
	}
}
