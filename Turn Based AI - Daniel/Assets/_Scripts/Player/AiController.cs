using System.Threading.Tasks;
using UnityEngine;


namespace DannyG
{
	
	public class AiController : PlayerController
	{
		private DifficultyLevel _difficultyLevel;
		private Minimax _minimax;
		

		private void SetDifficultyLevel()
		{
			int playerIdToArrayIndex = (int)PlayerData.PlayerId - 1;
			DifficultyNames difficultyName = SetupDataLocator.GameSetupData.selectedDifficulties[playerIdToArrayIndex];
			DifficultyLevelsDataSO difficultyLevelsData = SetupDataLocator.GameSetupData.difficultyLevelsData;
			_difficultyLevel = difficultyLevelsData.GetDifficultyLevel(difficultyName);
		}

		public override void Initialize(PlayerId id, PlayerType type)
		{
			base.Initialize(id, type);
			SetDifficultyLevel();
			_minimax = new Minimax(_difficultyLevel, ValidMovesCalculator, PlayerData.PlayerId, MakeAMove);
		}


		public override void StartTurn()
		{
			base.StartTurn();

			_minimax.StartTurn();
		}

		protected override void MakeAMove(Coordinate placeToMove)
		{
			// Don't know what to add here. Probably noop
			base.MakeAMove(placeToMove);
		}
	}
}
