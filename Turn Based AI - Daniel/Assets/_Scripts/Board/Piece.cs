using System;
using UnityEngine;


namespace DannyG
{
	/// <summary>
	/// For player pieces.
	/// </summary>
	public class Piece : Tile
	{
		public Coordinate coordinate { get; set; } = new Coordinate(0, 0);
		public override void Place(Vector3 startingPosition, Vector3 targetPosition, float overallScaleModifier)
		{
			base.Place(startingPosition, targetPosition, overallScaleModifier);
			Move(targetPosition);
		}

		public void Move(Vector3 targetPosition)
		{
			transform.position = targetPosition;
		}
		
		public void SetCoordinate(int x, int y)
		{
			coordinate.Update(x, y);
		}
		public void SetCoordinate(Coordinate coordinate)
		{
			this.coordinate = coordinate;
		}
		
	}
}
