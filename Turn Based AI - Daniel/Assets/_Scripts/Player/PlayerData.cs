using UnityEngine;


namespace DannyG
{
    public struct PlayerData
    {
        public PlayerId PlayerId;
        public PlayerType PlayerType;

        public PlayerData(PlayerId playerId, PlayerType playerType)
        {
            PlayerId = playerId;
            PlayerType = playerType;
        }
    }
    
	public enum PlayerId
    {
        Player1 = 1,
        Player2 = 2
    }
    
	public enum PlayerType
	{
		Human,
		Ai
	}
}