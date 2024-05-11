using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityUtils;


namespace DannyG
{
	
	public class SetupMenuManager : Singleton<SetupMenuManager>
	{
		private MapPresetsSO _mapPresets;
		

		// Start is called before the first frame update
		void Awake()
		{
			_mapPresets = Resources.Load<MapPresetsSO>("Map Presets");
			_mapPresets.Init();
			//CreateGameSetupData(); // Called from level manager for testing purposes
		}
		public void CreateGameSetupData() // change to private later
		{
			 GameSetupDataSO gameSetupData = Resources.Load<GameSetupDataSO>("Game Setup Data");

			gameSetupData.startingGrid = _mapPresets.ChooseRandomMap();
			gameSetupData.SelectRandomStartingPlayer();
		}
		
		

	}
}
