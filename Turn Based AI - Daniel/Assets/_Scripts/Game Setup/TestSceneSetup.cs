using System;
using System.Collections;
using UnityEngine;


namespace DannyG
{
	
	public class TestSceneSetup : MonoBehaviour
	{
		private void Awake()
		{
			CreateTestSetupData();
		}
		private void CreateTestSetupData()
		{
			GameSetupDataSO gameSetupData = Resources.Load<GameSetupDataSO>("Game Setup Data");

			gameSetupData.ChosenMapIndex = -1;
			gameSetupData.SelectRandomStartingPlayer();
			gameSetupData.player1Type = PlayerType.Human;
			gameSetupData.player2Type = PlayerType.Human;
		}
	}
}
