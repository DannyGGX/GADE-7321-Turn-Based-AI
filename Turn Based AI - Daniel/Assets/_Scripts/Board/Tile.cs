using UnityEngine;


namespace DannyG
{
    /// <summary>
    /// For empty tiles and pre-placed blockers
    /// </summary>
    public class Tile : MonoBehaviour
    {
        [field: SerializeField] public TileType tileType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startingPosition">  </param>
        /// <param name="targetPosition"> Not used for tiles, only for placed pieces </param>
        /// <param name="overallScaleModifier"></param>
        public virtual void Place(Vector3 startingPosition, Vector3 targetPosition, float overallScaleModifier)
        {
            var transform1 = transform;
            transform1.localScale *= overallScaleModifier;
            transform1.position = startingPosition;
        }
        
        
    }
}