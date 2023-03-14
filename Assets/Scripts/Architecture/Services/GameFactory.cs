using Obstacles;
using Player;
using TMPro;
using UI;
using UnityEngine;

namespace Architecture.Services
{
    public class GameFactory
    {
        private const string TowerPath = "Prefabs/Tower";
        private const string PlayerPath = "Prefabs/Player";
        private const string ProjectilePath = "Prefabs/Projectile";
        private const string ObstaclePath = "Prefabs/Obstacle";
        private const string UIPath = "Prefabs/UI";

        private Tower _tower;
        private PlayerController _player;
        private readonly AssetProvider _assetProvider;
        private readonly SceneLoader _sceneLoader;

        public GameFactory(AssetProvider assetProvider, SceneLoader sceneLoader)
        {
            _assetProvider = assetProvider;
            _sceneLoader = sceneLoader;
        }

        public void CreateTower(Vector3 position)
        {
            _tower = Object.Instantiate(_assetProvider.LoadObject(TowerPath),
                position, Quaternion.identity).GetComponent<Tower>();
        }

        public void CreatePlayer(Vector3 position)
        {
            _player = Object.Instantiate(_assetProvider.LoadObject(PlayerPath), position, Quaternion.identity)
                .GetComponent<PlayerController>();
            _player.Initialize(_tower, this);
        }

        public Projectile CreateProjectile(Vector3 position)
        {
            return Object.Instantiate(_assetProvider.LoadObject(ProjectilePath), position, Quaternion.identity)
                    .GetComponent<Projectile>();
        }

        public Obstacle CreateObstacle(Transform parent)
        {
            return Object.Instantiate(_assetProvider.LoadObject(ObstaclePath), parent).GetComponent<Obstacle>();
        }
        
        public void CreateUI()
        {
            var ui = Object.Instantiate(_assetProvider.LoadObject(UIPath)).GetComponent<UIController>();
            ui.Initialize(_player, _sceneLoader);
        }
    }
}
