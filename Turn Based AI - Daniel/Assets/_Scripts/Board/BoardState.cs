using System.Text;

namespace DannyG
{
    public struct BoardState
    {
        public int[,] grid { get; set; }
        
        public void PlacePiece(MoveData moveData)
        {
        	grid[moveData.Coordinate.x, moveData.Coordinate.y] = (int)moveData.PlayerId;
        }
        		
        public void ShiftPieces(AllShiftedTilesData allShiftedTiles)
        {
        	int[] currentLineOfTileData;
	        var state = this;
        	
        	foreach (var line in allShiftedTiles.listOfShiftedTiles)
        	{
        		currentLineOfTileData = new int[line.count];
        		for (int index = 0; index < line.count; index++)
        		{
        			int x = line.lineOfTiles[index].x;
        			int y = line.lineOfTiles[index].y;
        			currentLineOfTileData[index] = grid[x, y];
        			grid[x, y] = (int)TileType.Empty;
        		}
        		PlaceTiles(line);
        	}
			return;

	        void PlaceTiles(ShiftTilesLine line)
        	{
        		Incrementor2D incrementor = line.shiftAmount.Normalize();
        		incrementor.SetOpposite();
        		int x = line.lineOfTiles[0].x + line.shiftAmount.x;
        		int y = line.lineOfTiles[0].y + line.shiftAmount.y;
        		Coordinate currentCoordinate = new Coordinate(x, y);

        		for (int index = 0; index < line.count; index++)
        		{
			        state.grid[currentCoordinate.x, currentCoordinate.y] = currentLineOfTileData[index];
        			currentCoordinate.Increment(incrementor);
        		}
        	}
        	
        }

        public override string ToString()
        {
        	StringBuilder stringBuilder = new StringBuilder();
        	stringBuilder.Append("\n");
        	for (int y = grid.GetLength(1) - 1; y >= 0; y--)
        	{
        		for (int x = 0; x < grid.GetLength(0); x++)
        		{
        			stringBuilder.Append($"{grid[x, y]} ");
        		}
        		stringBuilder.Append("\n");
        	}
        	return stringBuilder.ToString();
        }
        
		public static bool operator ==(BoardState left, BoardState right)
		{
			return left.grid == right.grid;
		}
		public static bool operator !=(BoardState left, BoardState right)
		{
			return left.grid != right.grid;
		}

		public TileType GetTileTypeAt(Coordinate coordinate)
		{
			return (TileType)grid[coordinate.x, coordinate.y];
		}
		
		public bool IsInBounds(Coordinate coordinate)
		{
			return coordinate is { x: >= 0, y: >= 0 } && coordinate.x < grid.GetLength(0) && coordinate.y < grid.GetLength(1);
		}
    }
}