using UnityEngine;
using UnityUtils;


namespace DannyG
{
	
	public class GravityShiftBoardCalculator : Singleton<GravityShiftBoardCalculator>
	{
		
		
		private void OnEnable()
		{
			EventManager.onGravityShift.Subscribe(ShiftGravity);
		}
		private void OnDisable()
		{
			EventManager.onGravityShift.Unsubscribe(ShiftGravity);
		}

		private void ShiftGravity()
		{
			GravityManager.NextGravityState();
		}
	}
}
