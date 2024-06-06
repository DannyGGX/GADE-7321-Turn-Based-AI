using System;
using System.Collections.Generic;

namespace DannyG
{
    public class Minimax
    {
        private readonly int _maxDepth;
        private readonly ValidMovesCalculator _validMovesCalculator;
        private readonly UtilityFunction _utilityFunction;
        private readonly PlayerId _maximizingPlayerId;
        private readonly PlayerId _minimizingPlayerId;
        
        private float _maxEvaluationScore = float.MinValue;
        private float _minEvaluationScore = float.MaxValue;
        private MoveData _currentMoveData;

        public Minimax(int maxDepth, ValidMovesCalculator validMovesCalculator, PlayerId maximizingPlayerId)
        {
            _maxDepth = maxDepth;
            _validMovesCalculator = validMovesCalculator;
            _utilityFunction = new UtilityFunction();
            this._maximizingPlayerId = maximizingPlayerId;
            this._minimizingPlayerId = maximizingPlayerId == PlayerId.Player1 ? PlayerId.Player2 : PlayerId.Player1;
        }


        public void StartTurn()
        {
            MiniMax(BoardStateManager.boardState, _maxDepth, true);
        }
        
        /// <summary>
        /// Recursive minimax function
        /// </summary>
        /// <returns> The evaluation score </returns>
        private float MiniMax(BoardState boardState, int depth, bool isMaximizingPlayer)
        {
            if (depth == 0 || IsGameOver(boardState, _currentMoveData))
            {
                return _utilityFunction.Evaluate(boardState, _currentMoveData, isMaximizingPlayer);
            }
            
            float currentEvaluationScore;
            List<Coordinate> validMoves = _validMovesCalculator.GetValidMoves(boardState);
            
            if (isMaximizingPlayer)
            {
                currentEvaluationScore = float.MinValue;
                _maxEvaluationScore = float.MinValue;
                foreach (var move in validMoves)
                {
                    _currentMoveData = new MoveData(move, _maximizingPlayerId);
                    boardState.PlacePiece(_currentMoveData);
                    currentEvaluationScore = MiniMax(boardState, depth - 1, false);
                    _maxEvaluationScore = Max(currentEvaluationScore, _maxEvaluationScore);
                    //boardState.RemovePieceAt(move);
                }
                return _maxEvaluationScore;
            }
            else
            {
                currentEvaluationScore = float.MaxValue;
                _minEvaluationScore = float.MaxValue;
                foreach (var move in validMoves)
                {
                    _currentMoveData = new MoveData(move, _minimizingPlayerId);
                    boardState.PlacePiece(_currentMoveData);
                    currentEvaluationScore = MiniMax(boardState, depth - 1, true);
                    _minEvaluationScore = Min(currentEvaluationScore, _minEvaluationScore);
                    //boardState.RemovePieceAt(move);
                }
                return _minEvaluationScore;
            }
        }


        private static bool IsGameOver(BoardState state, MoveData moveData)
        {
            // Don't know if this other way works better or is less processing intensive
            //return WinChecker.Instance.WholeBoardWinCheck(state) || WinChecker.IsDrawState(state);
            
            return WinChecker.Instance.CheckForWinAroundPiece(moveData.Coordinate, state) || WinChecker.IsDrawState(state);
        }
        
        private static float Max(float a, float b)
        {
            return a > b ? a : b;
        }
        private static float Min(float a, float b)
        {
            return a < b ? a : b;
        }
    }
    
}