using UnityEngine;
using UnityEditor;

namespace DannyG
{
#if UNITY_EDITOR
	
	public static class BoardDebug
	{
		[MenuItem("Debug/Print Board State")]
		public static void PrintBoardState()
		{
			if (Application.isPlaying == false)
			{
				Debug.Log("Application needs to be running to see board state.");
				return;
			}
			Debug.Log(BoardStateManager.Instance.ToString());
		}
		
	}

#endif
}
