using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityUtils;


namespace DannyG
{
	
	public class ValidMovesDebugText : Singleton<ValidMovesDebugText>
	{
		private TextMeshProUGUI _text;
		readonly StringBuilder _stringBuilder = new StringBuilder();
		protected override void Awake()
		{
			base.Awake();
			_text = GetComponent<TextMeshProUGUI>();
		}
		
		public void SetText(List<Coordinate> validMoves)
		{
			for (int i = 0; i < validMoves.Count; i++)
			{
				_stringBuilder.Append($"({validMoves[i].x}, {validMoves[i].y})\n");
			}
			_text.text = _stringBuilder.ToString();
		}
	}
}
