using System.Collections.Generic;
using UnityEngine;

namespace DannyG
{
    public class BoardDisplayData
    {
        // input variables
        private int _cellsWide; // number of cells wide
        private int _cellsHigh; // number of cells high
        private float _frameWidth;
        private float _frameHeight;
        private float _margin;
        private Vector3 _centerPosition;
        
        // calculation variables
        private float _tileLength;
        private float _boardWidth;
        private float _boardHeight;
        private const int ExtraEdge = 1;
        private Vector3 _originTileCenterPosition;
        private const float TileLengthToScaleMultiplier = 1 / 1.125f; // 1 scale to a tile length of 1.125
        
        // output variables
        public Vector3[,] tileCenterPositions { get; private set; } // starting from bottom left
        public float overallScaleModifier { get; private set; }
        
        /// <summary>
        /// Positions where tokens are dropped from (1 tile length away from the edges)
        /// 0 = top (gravity state = bottom)
        /// 1 = right (gravity state = left)
        /// 2 = bottom (gravity state = top)
        /// 3 = left (gravity state = right)
        ///
        /// horizontal positions are ordered left to right
        /// vertical positions are ordered bottom to top
        /// 
        /// e.g. StartingPositions[0, 3] is the top edge, 3rd cell from the left
        /// </summary>
        public Dictionary<GravityStates, Vector3[]> StartingPositions;
        
        public BoardDisplayData(int cellsWide, int cellsHigh, float frameWidth, float frameHeight, float margin, Vector3 centerPosition)
        {
            _cellsWide = cellsWide;
            _cellsHigh = cellsHigh;
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _margin = margin;
            _centerPosition = centerPosition;
            
            CalculateTileLength();
            CalculateBoardDimensions();
            CalculateOriginTileCenterPosition();
            CalculateTileCenterPositions();
            CalculateOverallScaleModifier();
            CalculateStartingPositions();
        }

        private void CalculateTileLength()
        {
            // Calculate tile length using the height
            _tileLength = CalculateTileLengthWithFrameEdge(_frameHeight, _cellsHigh);

            if (CalculateBoardWidth() > _frameWidth)
            {
                _tileLength = CalculateTileLengthWithFrameEdge(_frameWidth, _cellsWide);
            }
            float CalculateTileLengthWithFrameEdge(float frameEdge, int numberOfCellsInEdge)
            {
                
                float remainingDistance = frameEdge - (_margin * 2);
                
                remainingDistance = remainingDistance / (numberOfCellsInEdge + ExtraEdge);
                return remainingDistance;
            }
        }

        private void CalculateBoardDimensions()
        {
            _boardWidth = CalculateBoardWidth();
            _boardHeight = CalculateBoardHeight();
        }
        
        private float CalculateBoardWidth()
        {
            return (_cellsWide + ExtraEdge) * _tileLength;
        }
        private float CalculateBoardHeight()
        {
            return (_cellsHigh + ExtraEdge) * _tileLength;
        }
        
        private void CalculateOriginTileCenterPosition()
        {
            float xMidpoint = CalculateMidpointOfTileCenters(_cellsWide);
            float yMidpoint = CalculateMidpointOfTileCenters(_cellsHigh);
            _originTileCenterPosition = new Vector3(_centerPosition.x - xMidpoint, _centerPosition.y - yMidpoint);

            float CalculateMidpointOfTileCenters(float edgeCellNumber)
            {
                return edgeCellNumber * _tileLength / 2;
            }
        }
        
        private void CalculateTileCenterPositions()
        {
            tileCenterPositions = new Vector3[_cellsWide, _cellsHigh];
            Vector3 currentPosition = _originTileCenterPosition;
            
            for (int y = 0; y < _cellsHigh; y++)
            {
                for (int x = 0; x < _cellsWide; x++)
                {
                    currentPosition.x = _originTileCenterPosition.x + (x * _tileLength);
                    tileCenterPositions[x, y] = currentPosition;
                }
                currentPosition.y += _tileLength;
            }
        }

        private void CalculateOverallScaleModifier()
        {
            overallScaleModifier = _tileLength * TileLengthToScaleMultiplier;
        }

        private void CalculateStartingPositions()
        {
            // Define every edge
            var topStartingPositions = new Vector3[_cellsWide];
            var leftStartingPositions = new Vector3[_cellsHigh];
            var bottomStartingPositions = new Vector3[_cellsWide];
            var rightStartingPositions = new Vector3[_cellsHigh];
            
            // Calculate the first starting positions
            topStartingPositions[0] = tileCenterPositions[0, _cellsHigh - 1];
            topStartingPositions[0].y += _tileLength;
            
            leftStartingPositions[0] = tileCenterPositions[0, 0];
            leftStartingPositions[0].x -= _tileLength;
            
            bottomStartingPositions[0] = tileCenterPositions[0, 0];
            bottomStartingPositions[0].y -= _tileLength;
            
            rightStartingPositions[0] = tileCenterPositions[_cellsWide - 1, 0];
            rightStartingPositions[0].x += _tileLength;
            
            
            // Calculate the rest of the starting positions
            topStartingPositions = HorizontalStartingPositions(topStartingPositions[0]);
            leftStartingPositions = VerticalStartingPositions(leftStartingPositions[0]);
            bottomStartingPositions = HorizontalStartingPositions(bottomStartingPositions[0]);
            rightStartingPositions = VerticalStartingPositions(rightStartingPositions[0]);
            
            // Add all the vector arrays to the dictionary
            StartingPositions = new Dictionary<GravityStates, Vector3[]>
            {
                { GravityStates.Down, topStartingPositions },
                { GravityStates.Right, leftStartingPositions },
                { GravityStates.Up, bottomStartingPositions },
                { GravityStates.Left, rightStartingPositions }
            };

            Vector3[] VerticalStartingPositions(Vector3 firstPosition)
            {
                var positions = new Vector3[_cellsHigh];
                positions[0] = firstPosition;
                for (int y = 1; y < _cellsHigh; y++)
                {
                    positions[y] = positions[y - 1]; // set to previous position in the loop
                    positions[y].y += _tileLength;
                }
                return positions;
            }
            Vector3[] HorizontalStartingPositions(Vector3 firstPosition)
            {
                var positions = new Vector3[_cellsHigh];
                positions[0] = firstPosition;
                for (int x = 1; x < _cellsWide; x++)
                {
                    positions[x] = positions[x - 1]; // set to previous position in the loop
                    positions[x].x += _tileLength;
                }
                return positions;
            }
        }
        
    }
}