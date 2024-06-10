using System;

namespace DannyG
{
    public class UtilityFunction
    {

        public UtilityFunction(bool hasCheckForMultipleLinesOf3, bool hasCountNumberOfConnect4Possibilities)
        {
            
        }
        
        public float Evaluate(BoardState boardState, MoveData moveData , bool isMaximizingPlayer)
        {
            float result = CalculateLineOfPiecesUtility();
            result *= CalculatePlayerUtility();
            return result;
            
            int CalculateLineOfPiecesUtility()
            {
                int lineOfPiecesResult = LineOfPiecesOperations.GetLongestLineOfTilesInArea(moveData.Coordinate, boardState);
                return (int)Math.Pow(lineOfPiecesResult, 2);
            }
            float CalculatePlayerUtility()
            {
                return isMaximizingPlayer? 1 : -1;
            }
        }
    }
}