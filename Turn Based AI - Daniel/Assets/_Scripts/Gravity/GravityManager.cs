using UnityEngine;
using UnityUtils;


namespace DannyG
{
	
	public static class GravityManager
	{
		public static GravityStates currentGravityState { get; private set; }
		
		public static void Init()
		{
			currentGravityState = GravityStates.Down;
		}
		
		public static void NextGravityState()
		{
			//currentGravityState = currentGravityState.Next();
		}
	}
}
