using System;
using System.Collections;
using UnityEngine;
using UnityUtils;


namespace DannyG
{
	
	public class TurnManager : Singleton<TurnManager>
	{
		[SerializeField] private int turnsBeforeGravityShift = 3;
		public int TurnsBeforeGravityShift => turnsBeforeGravityShift;
		
		private int _turnCount = 0;
		private bool _canStartTurn = true;
		private bool _hasGravityShiftedInCurrentTurn = false;
		private PlayerId _currentPlayer; 
		public PlayerId currentPlayer => _currentPlayer;
		
		private void OnEnable()
		{
			EventManager.onBoardDisplayFinishedUpdating.Subscribe(OnDisplayFinishedUpdating);
			EventManager.onDrawGame.Subscribe(DontStartTurn);
			EventManager.onPlayerWin.Subscribe(PlayerWonGame);
		}
		private void OnDisable()
		{
			EventManager.onBoardDisplayFinishedUpdating.Unsubscribe(OnDisplayFinishedUpdating);
			EventManager.onDrawGame.Unsubscribe(DontStartTurn);
			EventManager.onPlayerWin.Unsubscribe(PlayerWonGame);
		}
		private IEnumerator Start()
		{
			yield return null;
			_currentPlayer = SetupDataLocator.GameSetupData.startingPlayer;
			StartTurn();
		}

		private void StartTurn()
		{
			_turnCount++;
			EventManager.onTurnStart.Invoke(_currentPlayer);
		}

		private void NextTurn()
		{
			if(!_canStartTurn) return;
			//_currentPlayer = (PlayerId)_currentPlayer.Next(); // this returned 0 on third turn. Next() needs to be improved
			_currentPlayer = _currentPlayer == PlayerId.Player1 ? PlayerId.Player2 : PlayerId.Player1;
			StartTurn();
		}
		
		private void OnDisplayFinishedUpdating()
		{
			if (_hasGravityShiftedInCurrentTurn == false && IsGravityShiftTurn())
			{
				_hasGravityShiftedInCurrentTurn = true;
				EventManager.onGravityShift.Invoke();
			}
			else // turn has ended
			{
				_hasGravityShiftedInCurrentTurn = false;
				NextTurn(); // once the board has finished updating, start the next turn
			}
		}

		private void PlayerWonGame(PlayerId playerId)
		{
			DontStartTurn();
		}
		
		private void DontStartTurn()
		{
			_canStartTurn = false;
		}
		
		private bool IsGravityShiftTurn()
		{
			return _turnCount % turnsBeforeGravityShift == 0;
		}
	}
}
