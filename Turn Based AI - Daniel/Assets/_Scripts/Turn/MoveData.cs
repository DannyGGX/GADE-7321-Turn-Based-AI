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
    }
}