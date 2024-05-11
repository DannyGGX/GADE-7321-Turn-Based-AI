using UnityEngine;


namespace DannyG
{
	
	public class PlayerController : MonoBehaviour
	{
		protected PlayerData PlayerData;

		public virtual void Initialize(PlayerId id, PlayerType type)
		{
			PlayerData = new PlayerData(id, type);
		}
		
		private void OnEnable()
		{
			
		}
		private void OnDisable()
		{
			
		}
		
		
	}
}
