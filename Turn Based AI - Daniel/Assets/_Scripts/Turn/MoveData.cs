using System;

namespace DannyG
{
    public struct MoveData
    {
        public Coordinate Coordinate;
        public PlayerId PlayerId;

        public MoveData(Coordinate coordinate, PlayerId playerId)
        {
            Coordinate = coordinate;
            PlayerId = playerId;
        }
        
        public static TileType ConvertToTileType(PlayerId playerId)
        {
            switch (playerId)
            {
                case PlayerId.Player1:
                    return TileType.Player1Token;
                case PlayerId.Player2:
                    return TileType.Player2Token;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerId), playerId, null);
            }
        }
    }
}