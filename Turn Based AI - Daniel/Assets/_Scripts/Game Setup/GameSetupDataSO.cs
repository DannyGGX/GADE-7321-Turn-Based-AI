using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


namespace DannyG
{
	[CreateAssetMenu(fileName = "Game Setup Data", menuName = "Scriptable Object/Game Setup Data", order = 1)]
	public class GameSetupDataSO : ScriptableObject
	{
		[field: Header("Player Data")]
		[field: SerializeField] public PlayerId startingPlayer { get; set; }
		[field: SerializeField] public PlayerType player1Type { get; set; }
		[field: SerializeField] public PlayerType player2Type { get; set; }
		
		[Header("AI Data")]
		public DifficultyLevelsDataSO difficultyLevelsData;

		/// <summary>
		/// Player 1 index is 0, player 2 index is 1
		/// </summary>
		public DifficultyNames[] playerDifficulties = 
			new DifficultyNames[2] { DifficultyNames.Easy, DifficultyNames.Easy };

		[Header("Map Data")]
		[SerializeField] private MapPresetsSO _mapPresets;

		[field: SerializeField, Min(-1), Tooltip("-1 for random map")] 
		public int ChosenMapIndex { get; set; } = -1;
		
		// Board data
		public int[,] startingGrid { get; private set; }
		public Vector3[,] tileCenterPositions { get; private set; }
		public float overallScaleModifier { get; private set; }
		public Dictionary<GravityStates, Vector3[]> startingPositions { get; private set; }
		
		public TileDisplayFactorySO tileFactory;

		
		[Header("Board Display Data (in world coordinates)")]
		[SerializeField] private Vector3 centerPosition = Vector3.zero;
		[SerializeField] private float frameWidth = 10;
		[SerializeField] private float frameHeight = 10;
		[SerializeField] private float margin = 0.1f;

		
		public void Init()
		{
			_mapPresets.Init();
			try
			{
				startingGrid = _mapPresets.ChooseMap(ChosenMapIndex);
			}
			catch (Exception e)
			{
				Debug.LogError("Map Index out of range. Index: " + ChosenMapIndex);
			}
			
			BoardDisplayData boardDisplayData = new BoardDisplayData
			(
				startingGrid.GetLength(0), 
                startingGrid.GetLength(1), 
				frameWidth, 
				frameHeight, 
				margin, 
				centerPosition
			);

			tileCenterPositions = boardDisplayData.tileCenterPositions;
			overallScaleModifier = boardDisplayData.overallScaleModifier;
			startingPositions = boardDisplayData.StartingPositions;
			
			tileFactory.Init(this);
		}

		public void SelectRandomStartingPlayer()
		{
			int randomIndex = Random.Range(1, 3);
			startingPlayer = (PlayerId)randomIndex;
		}
	}
}
