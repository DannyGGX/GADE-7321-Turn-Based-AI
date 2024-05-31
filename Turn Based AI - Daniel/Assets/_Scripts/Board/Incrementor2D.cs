
using System;

namespace DannyG
{
	
	public struct Incrementor2D
	{
		public int x;
		public int y;

		public Incrementor2D(int x = 0, int y = 0)
		{
			this.x = x;
			this.y = y;
		}

		public Incrementor2D(Coordinate coordinateA, Coordinate coordinateB)
		{
			this = Incrementor2DExtensions.CreateWithDifferenceInCoordinates(coordinateA, coordinateB);
		}

		public void SetOpposite()
		{
			x = x * -1;
			y = y * -1;
		}

		public void Update(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public static bool operator >(Incrementor2D a, Incrementor2D b)
		{
			return a.x > b.x || a.y > b.y;
		}

		public static bool operator <(Incrementor2D a, Incrementor2D b)
		{
			return a.x < b.x || a.y < b.y;
		}
		
		public int GetMagnitude()
		{
			return Math.Abs(x) + Math.Abs(y);
		}

		
	}
	public static class Incrementor2DExtensions
	{
		public static Incrementor2D ToIncrementor2D(this Coordinate coordinate)
		{
			return new Incrementor2D(coordinate.x, coordinate.y);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="incrementor"></param>
		/// <returns> Output: -1, 0 or 1 for x and y values </returns>
		public static Incrementor2D Normalize(this Incrementor2D incrementor)
		{
			return new Incrementor2D(NormalizeSingleCoordinate(incrementor.x), NormalizeSingleCoordinate(incrementor.y));

		}
		public static void NormalizeWithoutOutput(this Incrementor2D incrementor)
		{
			incrementor.Update(NormalizeSingleCoordinate(incrementor.x), NormalizeSingleCoordinate(incrementor.y));
		}
		
		private static int NormalizeSingleCoordinate(int value)
		{
			return value switch
			{
				> 0 => 1,
				< 0 => -1,
				_ => 0
			};
		}
		
		public static Incrementor2D CreateWithDifferenceInCoordinates(Coordinate coordinateA, Coordinate coordinateB)
		{
			Coordinate result = new Coordinate();
			result.Update(coordinateA - coordinateB);
			return result.ToIncrementor2D();
		}
	}
}
