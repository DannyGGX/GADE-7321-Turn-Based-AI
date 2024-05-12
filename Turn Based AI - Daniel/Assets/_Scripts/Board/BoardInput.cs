using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityUtils;


namespace DannyG
{
	/// <summary>
	/// 2 humans can use the same board input.
	/// </summary>
	public class BoardInput : Singleton<BoardInput>
	{
		[SerializeField] private BoardButton buttonPrefab;

		private BoardButton[] _buttons;
		private PlayerId currentPlayer;
		private GameSetupDataSO _gameSetupData;
		private Camera _mainCamera;
		
		private void OnEnable()
		{
			_mainCamera = Camera.main;
			_gameSetupData = SetupDataLocator.GameSetupData;
			var rows = _gameSetupData.startingGrid.GetLength(0);
			var columns = _gameSetupData.startingGrid.GetLength(1);
			var scaleModifier = _gameSetupData.overallScaleModifier;
			CreateButtons(rows, columns, scaleModifier);
		}
		private void OnDisable()
		{
		}
		
		private void CreateButtons(int rows, int columns, float scaleModifier)
		{
			_buttons = new BoardButton[Max(rows, columns)];

			for (int buttonIndex = 0; buttonIndex < _buttons.Length; buttonIndex++)
			{
				_buttons[buttonIndex] = Instantiate(buttonPrefab, transform);
				_buttons[buttonIndex].transform.localScale = new Vector3(scaleModifier, scaleModifier, 1);
			}

			return;
			int Max(int a, int b)
			{
				return a > b ? a : b;
			}
		}

		public void InitializeBoardButtons(Action<Coordinate> clickButtonCallback)
		{
			foreach (var boardButton in _buttons)
			{
				boardButton.Initialize(clickButtonCallback);
			}
		}

		public void StartTurn(ValidMovesData validMoves)
		{
			for (int buttonIndex = 0; buttonIndex < validMoves.List.Count; buttonIndex++)
			{
				SetButtonPosition(buttonIndex, validMoves.List[buttonIndex]);
				_buttons[buttonIndex].SetCoordinate(validMoves.List[buttonIndex]);
				_buttons[buttonIndex].SetEnableState(true);
			}
		}
		
		public void DisableButtons()
		{
			foreach (var boardButton in _buttons)
			{
				boardButton.SetEnableState(false);
			}
		}
		
		private void SetButtonPosition(int buttonIndex, Coordinate coordinate)
		{
			Vector3 position =
				_mainCamera.WorldToScreenPoint(_gameSetupData.tileCenterPositions[coordinate.x, coordinate.y]);
			_buttons[buttonIndex].transform.position = position;
		}
	}
}
