using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityUtils;


namespace DannyG
{
	
	public class GravityShiftBoardCalculator : Singleton<GravityShiftBoardCalculator>
	{
		private int[,] _grid;
		private AllShiftedTilesData _allShiftedTilesData;
		private enum ShiftingStates
		{
			LookingForLandingPosition,
			LookingForPieces,
			RoundingUpPieces,
		}
		
		// dimension variables
		private int _targetDimensionLength;
		private int _otherDimensionLength;
		private delegate int GetCurrentXValue();
		private GetCurrentXValue _getCurrentX;
		private delegate int GetCurrentYValue();
		private GetCurrentYValue _getCurrentY;
		
		// loop direction variables
		private int _incrementor;
		private int _targetDimensionStartIndex;
		private int _targetDimensionEndIndex;
		private delegate bool LoopEndCondition(int index);
		private LoopEndCondition _endCondition;

		private void OnEnable()
		{
			EventManager.onGravityShift.Subscribe(ShiftGravity);
		}
		private void OnDisable()
		{
			EventManager.onGravityShift.Unsubscribe(ShiftGravity);
		}

		private void ShiftGravity()
		{
			GravityManager.NextGravityState();
			
			_grid = BoardStateManager.Instance.grid;
			GravityStates currentGravityState = GravityManager.currentGravityState;
			switch (currentGravityState)
			{
				case GravityStates.Right:
					ShiftPieces(true, true);
					break;
				case GravityStates.Up:
					ShiftPieces(false, true);
					break;
				case GravityStates.Left:
					ShiftPieces(true, false);
					break;
				case GravityStates.Down:
					ShiftPieces(false, false);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(currentGravityState), currentGravityState, null);
			}
			EventManager.onApplyGravityShiftToDisplay.Invoke(_allShiftedTilesData);
		}

		private void ShiftPieces(bool inXDimension, bool inReverseLoop)
		{
			Coordinate currentLandingPosition = new Coordinate();
			ShiftingStates currentState;
			_allShiftedTilesData = new AllShiftedTilesData(default);
			ShiftTilesLine currentLine = new ShiftTilesLine(default);
			
			int targetDimensionIndex = default;
			int otherDimensionIndex = default;
			int y;
			int x;
			SetDimensionVariables(inXDimension);
			SetLoopDirectionVariables(inReverseLoop);

			for (otherDimensionIndex = 0; otherDimensionIndex < _otherDimensionLength; otherDimensionIndex++)
			{
				currentState = ShiftingStates.LookingForLandingPosition;
				for (targetDimensionIndex = _targetDimensionStartIndex; _endCondition(targetDimensionIndex); targetDimensionIndex += _incrementor)
				{
					x = _getCurrentX.Invoke();
					y = _getCurrentY.Invoke();
					
					switch (currentState)
					{
						case ShiftingStates.LookingForLandingPosition:
							LookForLandingPosition();
							break;
						case ShiftingStates.LookingForPieces:
							LookForPieces();
							break;
						case ShiftingStates.RoundingUpPieces:
							RoundUpPieces();
							break;
					}
				}
				if (currentLine.count > 0) // this is done to try stop pieces from sticking to the edge of the board
				{
					_allShiftedTilesData.AddLine(currentLine);
					currentLine = new ShiftTilesLine(default);
				}
			}

			void LookForLandingPosition()
			{
				switch (_grid[x, y])
				{
					case (int)TileType.Empty:
						currentLandingPosition.Update(x, y);
						currentState = ShiftingStates.LookingForPieces;
						break;
					default:
						break;
				}
			}

			void LookForPieces()
			{
				switch (_grid[x, y])
				{
					case (int)TileType.Player1Token:
					case (int)TileType.Player2Token:
						currentState = ShiftingStates.RoundingUpPieces;
						Coordinate currentPiecePosition = new Coordinate(x, y);
						currentLine = new ShiftTilesLine(default);
						currentLine.CreateShiftAmount(currentPiecePosition, currentLandingPosition);
						currentLine.AddTile(currentPiecePosition, _grid[x, y]);
						break;
					case (int)TileType.Blocker:
						currentState = ShiftingStates.LookingForLandingPosition;
						if (currentLine.count > 0)
						{
							_allShiftedTilesData.AddLine(currentLine);
							currentLine = new ShiftTilesLine(default);
						}
						break;
					case (int)TileType.Empty:
						break;
				}
			}

			void RoundUpPieces()
			{
				switch (_grid[x, y])
				{
					case (int)TileType.Player1Token:
					case (int)TileType.Player2Token:
						Coordinate currentPiecePosition = new Coordinate(x, y);
						currentLine.AddTile(currentPiecePosition, _grid[x, y]);
						break;
					case (int)TileType.Empty:
						currentState = ShiftingStates.LookingForPieces;
						CreateLandingPositionOnShiftedPieces(_incrementor, inXDimension);
						_allShiftedTilesData.AddLine(currentLine);
						currentLine = new ShiftTilesLine(default);
						break;
					case (int)TileType.Blocker:
						currentState = ShiftingStates.LookingForLandingPosition;
						_allShiftedTilesData.AddLine(currentLine);
						currentLine = new ShiftTilesLine(default);
						break;
				}
			}

			void CreateLandingPositionOnShiftedPieces(int targetDimensionIncrementor, bool inXDimension)
			{
				int resultX = currentLandingPosition.x;
				int resultY = currentLandingPosition.y;
				if (inXDimension)
				{
					resultX += currentLine.count * targetDimensionIncrementor;
				}
				else
				{
					resultY += currentLine.count * targetDimensionIncrementor;
				}
				currentLandingPosition.Update(resultX, resultY);
			}

			
			
			void SetDimensionVariables(bool inXDimension)
			{
				if (inXDimension)
				{
					_targetDimensionLength = _grid.GetLength(0);
					_otherDimensionLength = _grid.GetLength(1);
					_getCurrentX = GetXValueForXTargetDimension;
					_getCurrentY = GetYValueForXTargetDimension;
				}
				else
				{
					_targetDimensionLength = _grid.GetLength(1);
					_otherDimensionLength = _grid.GetLength(0);
					_getCurrentX = GetXValueForYTargetDimension;
					_getCurrentY = GetYValueForYTargetDimension;
				}
			}
			void SetLoopDirectionVariables(bool inReverseLoop)
			{
				if (inReverseLoop)
				{
					_incrementor = -1;
					_targetDimensionStartIndex = _targetDimensionLength - 1;
					_targetDimensionEndIndex = 0;
					_endCondition = ReverseLoopEndCondition;
				}
				else
				{
					_incrementor = 1;
					_targetDimensionStartIndex = 0;
					_targetDimensionEndIndex = _targetDimensionLength - 1;
					_endCondition = ForwardLoopEndCondition;
				}
			}
			bool ForwardLoopEndCondition(int index) => index < _targetDimensionEndIndex;
			bool ReverseLoopEndCondition(int index) => index >= _targetDimensionEndIndex;

			int GetXValueForYTargetDimension() => otherDimensionIndex;
			int GetXValueForXTargetDimension() => targetDimensionIndex;
			int GetYValueForYTargetDimension() => targetDimensionIndex;
			int GetYValueForXTargetDimension() => otherDimensionIndex;
		}
		
		
	}
}
