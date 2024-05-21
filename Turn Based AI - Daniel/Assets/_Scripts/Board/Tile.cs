using UnityEngine;


namespace DannyG
{
    /// <summary>
    /// For empty tiles and pre-placed blockers
    /// </summary>
    public class Tile : MonoBehaviour
    {
        [field: SerializeField] public TileType tileType { get; private set; }
        
        public void ApplyScale(float overallScaleModifier)
        {
            transform.localScale *= overallScaleModifier;
        }
        
    }
}