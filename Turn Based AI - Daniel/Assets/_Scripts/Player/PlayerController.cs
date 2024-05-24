using System.Collections.Generic;
using UnityEngine;


namespace DannyG
{
	
	public class PlayerController : MonoBehaviour
	{
		protected PlayerData PlayerData;
		protected ValidMovesCalculator ValidMovesCalculator;
		protected List<Coordinate> CurrentTurnValidMoves;
		protected MoveData ChosenMove;

		public virtual void Initialize(PlayerId id, PlayerType type)
		{
			PlayerData = new PlayerData(id, type);
			ValidMovesCalculator = new ValidMovesCalculator();
		}

		public virtual void StartTurn()
		{
			// Don't know what to add here. Probably noop
		}

		/// <summary>
		/// Add the base.MakeAMove after the overriden logic
		/// </summary>
		/// <param name="placeToMove"></param>
		protected virtual void MakeAMove(Coordinate placeToMove)
		{
			ChosenMove = new MoveData(placeToMove, PlayerData.PlayerId);
			EventManager.onPlacePiece.Invoke(ChosenMove);
		}
	}
}
