using System;
using Architecture.Services;
using Obstacles;
using UnityEngine;
using Zenject;

namespace Architecture
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private Transform playerPlace;
        [SerializeField] private Transform towerPlace;
        [SerializeField] private ObstacleSpawner obstacleSpawner;

        [Inject] private ServiceRegistrator _services;

        private void Awake()
        {
            obstacleSpawner.Initialize(_services.GameFactory);
        }

        private void Start()
        {
            _services.GameFactory.CreateTower(towerPlace.position);
            _services.GameFactory.CreatePlayer(playerPlace.position);
            _services.GameFactory.CreateUI();
            obstacleSpawner.SpawnObstacles();
        }
    }
}
