using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityUtils;


namespace DannyG
{
	
	public class SetupMenuManager : Singleton<SetupMenuManager>
	{
		private GameSetupDataSO _gameSetupData;

		public void CreateGameSetupData() // change to private later
		{
			_gameSetupData = Resources.Load<GameSetupDataSO>("Game Setup Data");
		}
		
	}
}
