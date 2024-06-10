using System;
using System.Collections.Generic;
using UnityEngine;

namespace DannyG
{
    public class Minimax
    {
        private readonly int _maxDepth;
        private readonly ValidMovesCalculator _validMovesCalculator;
        private readonly UtilityFunction _utilityFunction;
        private readonly PlayerId _maximizingPlayerId;
        private readonly PlayerId _minimizingPlayerId;
        private readonly Action<Coordinate> _moveCallback;
        
        private float _maxEvaluationScore = float.MinValue;
        private float _minEvaluationScore = float.MaxValue;
        private MoveData _currentMoveData;
        private BoardState _currentBoardState;

        public Minimax(DifficultyLevel difficultyLevel, ValidMovesCalculator validMovesCalculator, PlayerId maximizingPlayerId, Action<Coordinate> moveCallback)
        {
            _maxDepth = difficultyLevel.maxDepth;
            _validMovesCalculator = validMovesCalculator;
            _utilityFunction = new UtilityFunction
            (
                difficultyLevel.hasCheckForMultipleLinesOf3, 
                difficultyLevel.hasCountNumberOfConnect4Possibilities
            );
            this._maximizingPlayerId = maximizingPlayerId;
            this._minimizingPlayerId = maximizingPlayerId == PlayerId.Player1 ? PlayerId.Player2 : PlayerId.Player1;
            _moveCallback = moveCallback;
        }


        public void StartTurn()
        {
            MiniMax(BoardStateManager.boardState, _maxDepth, true);
            _moveCallback.Invoke(_currentMoveData.Coordinate);
        }
        
        /// <summary>
        /// Recursive minimax function
        /// </summary>
        /// <returns> The evaluation score </returns>
        private float MiniMax(BoardState boardState, int depth, bool isMaximizingPlayer)
        {
            if (depth == 0 || IsGameOver(boardState, _currentMoveData, depth))
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
                    _currentBoardState = new BoardState(boardState);
                    _currentBoardState.PlacePiece(_currentMoveData);
                    currentEvaluationScore = MiniMax(_currentBoardState, depth - 1, false);
                    _maxEvaluationScore = Max(currentEvaluationScore, _maxEvaluationScore);
                    _currentBoardState.RemovePieceAt(move);
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
                    _currentBoardState = new BoardState(boardState);
                    _currentBoardState.PlacePiece(_currentMoveData);
                    currentEvaluationScore = MiniMax(_currentBoardState, depth - 1, true);
                    _minEvaluationScore = Min(currentEvaluationScore, _minEvaluationScore);
                    _currentBoardState.RemovePieceAt(move);
                }
                return _minEvaluationScore;
            }
        }


        private bool IsGameOver(BoardState state, MoveData moveData, int currentDepth)
        {
            currentDepth = CalculateCurrentDepth(currentDepth);
            if (WinChecker.Instance.IsWinCheckThresholdReached(currentDepth)) return false;
            // Don't know if this other way works better or is less processing intensive
            return WinChecker.Instance.WholeBoardWinCheck(state) || WinChecker.Instance.IsDrawState(state, currentDepth);
            
            //This way doesn't work properly because when minimax runs for the first time in the turn, the move Data is not set, so coordinate is 0,0
            //return WinChecker.Instance.CheckForWinAroundPiece(moveData.Coordinate, state) || WinChecker.IsDrawState(state);

            int CalculateCurrentDepth(int countDownDepthValue)
            {
                return _maxDepth - countDownDepthValue;
            }
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