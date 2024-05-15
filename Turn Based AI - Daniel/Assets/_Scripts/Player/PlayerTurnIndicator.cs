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
		[SerializeField] private Image player1Image;
		[SerializeField] private Image player2Image;
		
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
			//colorIndicator.color = playerId == PlayerId.Player1 ? player1Color : player2Color; // this makes the image invisible
			if (playerId == PlayerId.Player1)
			{
				
				player1Image.enabled = true;
				player2Image.enabled = false;
				
			}
			else
			{
				player1Image.enabled = false;
				player2Image.enabled = true;
			}
		}
		
	}
}
