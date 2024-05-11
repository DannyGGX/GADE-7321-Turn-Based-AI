using UnityEngine;


namespace DannyG
{
	
	public static class SetupDataLocator
	{
		public static GameSetupDataSO GameSetupData { get; private set; }
		private const string FileName = "Game Setup Data";

		public static void Init()
		{
			GameSetupData = Resources.Load<GameSetupDataSO>(FileName);
			GameSetupData.Init();
		}
	}
}
