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
			Debug.Log(BoardStateManager.Instance.ToString());
		}
		
	}

#endif
}
