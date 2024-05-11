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
		}
		
		
	}
}
