using Architecture.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Obstacles
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private int obstacleCount;

        private const int XOffset = 15;
        private const int XRowCount = 30;
        private const float YPos = 0.5f;
        private const float RandomOffset = 0.4f;

        private GameFactory _gameFactory;

        public void Initialize(GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void SpawnObstacles()
        {
            float startXPos = transform.position.x - XOffset;
            int xOffset = 0;
            int zOffset = 1;
            for (int i = 0; i < obstacleCount; i++)
            {
                if (i >= XRowCount * zOffset)
                {
                    zOffset++;
                    xOffset = 0;
                }
                float xPos = startXPos + xOffset + Random.Range(-RandomOffset, RandomOffset);
                xOffset++;
                float zPos = transform.position.z - zOffset + Random.Range(-RandomOffset, RandomOffset);

                var obj = _gameFactory.CreateObstacle(transform);
                obj.transform.position = new Vector3(xPos, YPos, zPos);
            }
        }
    }
}
