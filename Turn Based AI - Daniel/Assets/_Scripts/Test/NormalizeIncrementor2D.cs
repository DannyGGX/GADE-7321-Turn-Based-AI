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
			AddToTestList();
			NormalizeTestList();
			//PrintResults();
		}
	}
}
