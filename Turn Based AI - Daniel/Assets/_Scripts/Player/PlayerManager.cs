using System;
using UnityEngine;
using UnityUtils;


namespace DannyG
{
	
	public class PlayerManager : Singleton<PlayerManager>
	{
		private PlayerController _player1Controller;
		private PlayerController _player2Controller;
		
		private void OnEnable()
		{
			EventManager.onTurnStart.Subscribe(StartTurn);
			
			_player1Controller = CreatePlayer(SetupDataLocator.GameSetupData.player1Type, PlayerId.Player1);
			_player2Controller = CreatePlayer(SetupDataLocator.GameSetupData.player2Type, PlayerId.Player2);
		}
		private void OnDisable()
		{
			EventManager.onTurnStart.Unsubscribe(StartTurn);
		}

		private PlayerController CreatePlayer(PlayerType playerType, PlayerId playerId)
		{
			PlayerController playerController;
			var obj = new GameObject(playerType + playerId.ToString());
			
			if (playerType == PlayerType.Human)
			{
				playerController = obj.AddComponent<HumanController>();
			}
			else //if (playerType == PlayerType.Ai)
			{
				playerController = obj.AddComponent<AiController>();
			}
			playerController.Initialize(playerId, playerType);
			return playerController;
		}
		
		private void StartTurn(PlayerId playerId)
		{
			if (playerId == PlayerId.Player1)
				_player1Controller.StartTurn();
			else
				_player2Controller.StartTurn();
		}
	}
}
