using System.Threading.Tasks;
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

		private async void ShiftGravity()
		{
			GravityManager.NextGravityState();
			
			// temporary
			await Task.Yield();
			EventManager.onBoardDisplayFinishedUpdating.Invoke();
		}
	}
}
