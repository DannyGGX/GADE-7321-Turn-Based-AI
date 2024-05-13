using UnityUtils;

namespace DannyG._Scripts.Scenes
{
    public class LevelManager : Singleton<LevelManager>
    {
        protected override void Awake()
        {
            base.Awake();
            SetupMenuManager.Instance.CreateGameSetupData();
            SetupDataLocator.Init();
            GravityManager.Init();
        }
    }
}