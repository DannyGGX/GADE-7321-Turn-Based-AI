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

			gameSetupData.ChosenMapIndex = 3;
			gameSetupData.startingPlayer = PlayerId.Player1;
			gameSetupData.player1Type = PlayerType.Human;
			gameSetupData.player2Type = PlayerType.Ai;
		}
	}
}
