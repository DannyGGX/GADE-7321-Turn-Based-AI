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
			if (TurnManager.Instance.currentPlayer != PlayerData.PlayerId) return;
			// If there are 2 instances of this class and the buttons for the input subscribe this method callback, then this will be called twice.
			
			await Task.Yield();
			BoardInput.Instance.DisableButtons();
			base.MakeAMove(placeToMove);
		}
		
	}
}
