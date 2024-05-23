
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

		public void SetOpposite()
		{
			x = x * -1;
			y = y * -1;
		}
		
	}
	public static class Incrementor2DExtensions
	{
		public static Incrementor2D ToIncrementor2D(this Coordinate coordinate)
		{
			return new Incrementor2D(coordinate.x, coordinate.y);
		}
	}
}
