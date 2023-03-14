using UnityEngine;

namespace Architecture.Services
{
    public class ServiceRegistrator : MonoBehaviour
    {
        public GameFactory GameFactory { get; private set; }
        public AssetProvider AssetProvider { get; private set; }
        
        public SceneLoader SceneLoader { get; private set; }
        public void Initialize()
        {
            SceneLoader = new SceneLoader();
            AssetProvider = new AssetProvider(); 
            GameFactory = new GameFactory(AssetProvider, SceneLoader);
        }
    }
}
