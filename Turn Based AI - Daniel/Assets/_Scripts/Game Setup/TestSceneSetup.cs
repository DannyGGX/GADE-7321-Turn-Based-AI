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
			if (MenuUI.oponentValue == 1 && MenuUI.mapValue == 1 && MenuUI.difficultyLevel == 1)
			{
				GameSetupDataSO gameSetupData = Resources.Load<GameSetupDataSO>("Game Setup Data");

				gameSetupData.ChosenMapIndex = 1;
				gameSetupData.SelectRandomStartingPlayer();
				gameSetupData.player1Type = PlayerType.Human;
				gameSetupData.player2Type = PlayerType.Human;
			}

            if (MenuUI.oponentValue == 1 && MenuUI.mapValue == 2 && MenuUI.difficultyLevel == 1)
            {
                GameSetupDataSO gameSetupData = Resources.Load<GameSetupDataSO>("Game Setup Data");

                gameSetupData.ChosenMapIndex = 3;
                gameSetupData.SelectRandomStartingPlayer();
                gameSetupData.player1Type = PlayerType.Human;
                gameSetupData.player2Type = PlayerType.Human;
            }

            if (MenuUI.oponentValue == 1 && MenuUI.mapValue == 3 && MenuUI.difficultyLevel == 1)
            {
                GameSetupDataSO gameSetupData = Resources.Load<GameSetupDataSO>("Game Setup Data");

                gameSetupData.ChosenMapIndex = 0;
                gameSetupData.SelectRandomStartingPlayer();
                gameSetupData.player1Type = PlayerType.Human;
                gameSetupData.player2Type = PlayerType.Human;
            }

            if (MenuUI.oponentValue == 2 && MenuUI.mapValue == 1 && MenuUI.difficultyLevel == 1)
            {
                GameSetupDataSO gameSetupData = Resources.Load<GameSetupDataSO>("Game Setup Data");

                gameSetupData.ChosenMapIndex = 1;
                gameSetupData.SelectRandomStartingPlayer();
                gameSetupData.player1Type = PlayerType.Human;
                gameSetupData.player2Type = PlayerType.Ai;
            }

            if (MenuUI.oponentValue == 2 && MenuUI.mapValue == 2 && MenuUI.difficultyLevel == 1)
            {
                GameSetupDataSO gameSetupData = Resources.Load<GameSetupDataSO>("Game Setup Data");

                gameSetupData.ChosenMapIndex = 3;
                gameSetupData.SelectRandomStartingPlayer();
                gameSetupData.player1Type = PlayerType.Human;
                gameSetupData.player2Type = PlayerType.Ai;
            }

            if (MenuUI.oponentValue == 2 && MenuUI.mapValue == 3 && MenuUI.difficultyLevel == 1)
            {
                GameSetupDataSO gameSetupData = Resources.Load<GameSetupDataSO>("Game Setup Data");

                gameSetupData.ChosenMapIndex = 0;
                gameSetupData.SelectRandomStartingPlayer();
                gameSetupData.player1Type = PlayerType.Human;
                gameSetupData.player2Type = PlayerType.Ai;
            }
        }
	}
}
