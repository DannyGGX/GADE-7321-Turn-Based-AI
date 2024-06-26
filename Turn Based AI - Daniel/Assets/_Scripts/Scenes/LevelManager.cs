using UnityUtils;

namespace DannyG._Scripts.Scenes
{
    public class LevelManager : Singleton<LevelManager>
    {
        protected override void Awake()
        {
            base.Awake();
            SetupDataLocator.Init();
            GravityManager.Init();
        }
    }
}