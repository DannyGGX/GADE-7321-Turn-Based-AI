using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace DannyG
{
	/// <summary>
	/// For displaying the board preset. It spawns the empty tiles and the blockers from the game setup data.
	/// It can be used for setup menu and for in-game.
	/// Use in the setup menu: Each preset needs a copy of this class.
	/// </summary>
	public class BoardSetupDisplay : MonoBehaviour
	{
		[SerializeField] private bool forSetupMenuUse = false;
		[SerializeField] private int mapPresetIndex = 0;
		
		private GameSetupDataSO _gameSetupData;

		private void OnEnable()
		{
			_gameSetupData = SetupDataLocator.GameSetupData;
			if (forSetupMenuUse == false)
			{
				mapPresetIndex = _gameSetupData.ChosenMapIndex;
			}
			CreateEmptyTilesAndStartingBlockers();
		}
		
		private void CreateEmptyTilesAndStartingBlockers()
		{
			int[,] mapPreset = _gameSetupData.mapPresets.ChooseMap(mapPresetIndex);
			TileDisplayFactorySO tileFactory = _gameSetupData.tileFactory;
			
			for (int y = 0; y < mapPreset.GetLength(1); y++)
			{
				for (int x = 0; x < mapPreset.GetLength(0); x++)
				{
					tileFactory.CreateTile(TileType.Empty, x, y);
					if (mapPreset[x, y] == (int)TileType.Blocker)
					{
						tileFactory.CreateTile(TileType.Blocker, x , y, transform);
					}
				}
			}
		}
	}
}
