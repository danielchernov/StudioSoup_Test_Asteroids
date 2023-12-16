using System.Collections;
using UnityEngine;

namespace AsteroidsTest.Game
{
    public class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField]
        float _minTimeToSpawn = 2;

        [SerializeField]
        float _maxTimeToSpawn = 5;

        float _playedTime = 0;
        float _screenWidth = 0;
        float _screenHeight = 0;

        [SerializeField]
        BoxCollider2D _screenBounds;

        void Start()
        {
            // Set Values up
            _screenWidth = Camera.main.aspect * Camera.main.orthographicSize;
            _screenHeight = Camera.main.orthographicSize;

            _playedTime = 0;

            // Start AsteroidSpawner Coroutine
            StartCoroutine(SpawnAsteroids());
        }

        void Update()
        {
            _playedTime += Time.deltaTime;
        }

        // Calculate Spawn Position
        Vector2 CalculateRandomPosition()
        {
            Vector2 spawnPosition = Vector2.zero;
            int side = Random.Range(0, 4);

            switch (side)
            {
                case 0: // top
                    spawnPosition = new Vector2(
                        Random.Range(-_screenWidth, _screenWidth),
                        _screenBounds.bounds.max.y
                    );
                    break;
                case 1: // right
                    spawnPosition = new Vector2(
                        _screenBounds.bounds.max.x,
                        Random.Range(-_screenHeight, _screenHeight)
                    );
                    break;
                case 2: // bottom
                    spawnPosition = new Vector2(
                        Random.Range(-_screenWidth, _screenWidth),
                        _screenBounds.bounds.min.y
                    );
                    break;
                case 3: // left
                    spawnPosition = new Vector2(
                        _screenBounds.bounds.min.x,
                        Random.Range(-_screenHeight, _screenHeight)
                    );
                    break;
            }

            return spawnPosition;
        }

        IEnumerator SpawnAsteroids()
        {
            // Time between Spawns
            float spawnSpeedModifier = 1 - Mathf.InverseLerp(0, 300, _playedTime);

            float timeToSpawn = Random.Range(
                _minTimeToSpawn * spawnSpeedModifier,
                _maxTimeToSpawn * spawnSpeedModifier
            );

            yield return new WaitForSeconds(timeToSpawn);

            // Calculate Asteroid Amount
            int ExtraAsteroids = (int)Mathf.Round(_playedTime / 30);
            int asteroidsToSpawn = Random.Range(0 + ExtraAsteroids, 2 + ExtraAsteroids);

            // Spawn Asteroids
            for (int i = 0; i < asteroidsToSpawn; i++)
            {
                Vector2 spawnHere = CalculateRandomPosition();

                // Choose Asteroid to Spawn
                string asteroidName;
                switch (Random.Range(0, 3))
                {
                    case 0:
                        asteroidName = "AsteroidSmall";
                        break;
                    case 1:
                        asteroidName = "AsteroidMedium";
                        break;
                    case 2:
                        asteroidName = "AsteroidLarge";
                        break;
                    default:
                        asteroidName = "AsteroidSmall";
                        break;
                }

                GameObject asteroid = ObjectPooler.Instance.SpawnFromPool(
                    asteroidName,
                    spawnHere,
                    Quaternion.identity
                );
            }

            yield return SpawnAsteroids();
        }
    }
}
