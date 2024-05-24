using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace DannyG
{
	[CreateAssetMenu(fileName = "Game Setup Data", menuName = "Scriptable Object/Game Setup Data", order = 1)]
	public class GameSetupDataSO : ScriptableObject
	{
		// Player data
		[field: SerializeField] public PlayerId startingPlayer { get; set; }
		[field: SerializeField] public PlayerType player1Type { get; set; }
		[field: SerializeField] public PlayerType player2Type { get; set; }
		
		// AI data
		public DifficultyLevelsDataSO difficultyLevelsData;

		/// <summary>
		/// Player 1 index is 0, player 2 index is 1
		/// </summary>
		public DifficultyNames[] playerDifficulties = 
			new DifficultyNames[2] { DifficultyNames.Easy, DifficultyNames.Easy };
		
		// Board data
		public int[,] startingGrid { get; set; }
		public Vector3[,] tileCenterPositions { get; private set; }
		public float overallScaleModifier { get; private set; }
		public Dictionary<GravityStates, Vector3[]> startingPositions { get; private set; }
		
		public TileDisplayFactorySO tileFactory;

		
		// Board input data
		[SerializeField] private Vector3 centerPosition = Vector3.zero;
		[SerializeField] private float frameWidth = 10;
		[SerializeField] private float frameHeight = 10;
		[SerializeField] private float margin = 0.1f;

		
		public void Init()
		{
			BoardDisplayData boardDisplayData = new BoardDisplayData(startingGrid.GetLength(0), startingGrid.GetLength(1), frameWidth, frameHeight, margin, centerPosition);

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
