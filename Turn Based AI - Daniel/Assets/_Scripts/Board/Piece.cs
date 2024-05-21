using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;


namespace DannyG
{
	/// <summary>
	/// For player pieces.
	/// </summary>
	public class Piece : Tile
	{
		private float distanceToTimeFactor = 20;
		public Coordinate coordinate { get; private set; }
		public override void Place(Vector3 startingPosition, Vector3 targetPosition, float overallScaleModifier)
		{
			base.Place(startingPosition, targetPosition, overallScaleModifier);
			//MoveTo(targetPosition);
		}

		public void MoveTo(Vector3 targetPosition, Action onCompleteCallback)
		{
			//transform.position = targetPosition;
			transform.DOMove(targetPosition, GetMoveTime(targetPosition)).SetEase(Ease.Linear).onComplete =
				onCompleteCallback.Invoke;
		}
		
		public void SetCoordinate(int x, int y)
		{
			coordinate.Update(x, y);
		}
		public void SetCoordinate(Coordinate coordinate)
		{
			this.coordinate = coordinate;
		}

		private float GetMoveTime(Vector3 targetPosition)
		{
			float distance = Vector3.Distance(transform.position, targetPosition);
			return distance / distanceToTimeFactor;
		}
	}
}
