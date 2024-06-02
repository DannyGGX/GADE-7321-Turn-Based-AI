using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;


namespace DannyG
{
	
	public class HumanController : PlayerController
	{
		private void Start()
		{
			BoardInput.Instance.InitializeBoardButtons(MakeAMove);
		}

		public override void StartTurn()
		{
			base.StartTurn();
			CurrentTurnValidMoves = ValidMovesCalculator.GetValidMoves();
			//ValidMovesDebugText.Instance.SetText(CurrentTurnValidMoves); // not needed any more
			BoardInput.Instance.StartTurn(new ValidMovesData(CurrentTurnValidMoves));
		}

		protected override async void MakeAMove(Coordinate placeToMove)
		{
			// Check that the correct human made the move, as both humans register this same callback method to the buttons.
			if (TurnManager.Instance.currentPlayer != PlayerData.PlayerId) return;
			
			// Wait a frame so that the buttons aren't immediately disabled before the input is registered.
			await Task.Yield();
			BoardInput.Instance.DisableButtons();
			base.MakeAMove(placeToMove);
		}
		
	}
}
