using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace DannyG
{
	[CreateAssetMenu(fileName = "Game Setup Data", menuName = "Scriptable Object/Game Setup Data", order = 1)]
	public class GameSetupDataSO : ScriptableObject
	{
		public PlayerId startingPlayer { get; set; }
		
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
