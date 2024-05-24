namespace DannyG
{
    public class Minimax
    {
        private int _maxDepth;
        private int _currentDepth;
        private ValidMovesCalculator _validMovesCalculator;
        private UtilityFunction _utilityFunction;

        public Minimax(int maxDepth, ValidMovesCalculator validMovesCalculator)
        {
            _maxDepth = maxDepth;
            _currentDepth = 0;
            _validMovesCalculator = validMovesCalculator;
        }


        public void StartTurn()
        {
            
        }
    }

    public class BoardState
    {
        
    }
    
}