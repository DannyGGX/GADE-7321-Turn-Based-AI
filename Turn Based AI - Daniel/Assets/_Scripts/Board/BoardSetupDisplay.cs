using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace DannyG
{
	/// <summary>
	/// For displaying the board preset. It spawns the empty tiles and the blockers from the game setup data.
	/// It can be used for setup menu and for in-game.
	/// </summary>
	public class BoardSetupDisplay : MonoBehaviour
	{
		private GameSetupDataSO _gameSetupData;
		private void OnEnable()
		{
			_gameSetupData = SetupDataLocator.GameSetupData;
			CreateEmptyTilesAndStartingBlockers();
		}

		private void CreateEmptyTilesAndStartingBlockers()
		{
			TileDisplayFactorySO tileFactory = _gameSetupData.tileFactory;
			
			for (int y = 0; y < _gameSetupData.startingGrid.GetLength(1); y++)
			{
				for (int x = 0; x < _gameSetupData.startingGrid.GetLength(0); x++)
				{
					tileFactory.CreateTile(TileType.Empty, x, y);
					if (_gameSetupData.startingGrid[x, y] == (int)TileType.Blocker)
					{
						tileFactory.CreateTile(TileType.Blocker, x , y, transform);
					}
				}
			}
		}
	}
}
