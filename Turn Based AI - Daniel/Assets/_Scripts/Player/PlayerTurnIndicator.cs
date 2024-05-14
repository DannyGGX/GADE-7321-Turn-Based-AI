using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace DannyG
{
	
	public class PlayerTurnIndicator : MonoBehaviour
	{
		[SerializeField] private Color player1Color;
		[SerializeField] private Color player2Color;
		
		[FormerlySerializedAs("_turnText")] [SerializeField] private TextMeshProUGUI turnText;
		[SerializeField] private Image colorIndicator;
		
		private void OnEnable()
		{
			//_turnText = GetComponent<TextMeshProUGUI>();
			
			EventManager.onTurnStart.Subscribe(ChangeIndicatorText);
		}

		private void OnDisable()
		{
			EventManager.onTurnStart.Unsubscribe(ChangeIndicatorText);
		}
		
		private void ChangeIndicatorText(PlayerId playerId)
		{
			turnText.text = $"Player {(int)playerId} Turn";
			colorIndicator.color = playerId == PlayerId.Player1 ? player1Color : player2Color;
		}
		
	}
}
