using System;
using System.Collections.Generic;
using Enemy;
using Installers;
using Player;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Misc
{
    public class EnemySpawner : ITickable, IInitializable
    {
        readonly EnemyFacade.Factory _enemyFactory;
        readonly Settings _settings;
        readonly GameInstaller.Settings _gameSettings;

        float _desiredNumSlowEnemies;
        int _enemyCount;
        float _lastSpawnTime;
        Camera _camera;
        private List<Vector3> _spawnedPositions = new List<Vector3>();

        public EnemySpawner(
            Settings settings,
            GameInstaller.Settings gameSettings,
            EnemyFacade.Factory enemyFactory,
            Camera camera)
        {
            _enemyFactory = enemyFactory;
            _settings = settings;
            _camera = camera;
            _gameSettings = gameSettings;

            _desiredNumSlowEnemies = settings.NumEnemiesSlowStartAmount;
        }

        public void Initialize()
        {
            // throw new NotImplementedException();
        }

        public void Tick()
        {
            _desiredNumSlowEnemies += _settings.NumEnemiesIncreaseRate * Time.deltaTime;

            if (_enemyCount < (int)_desiredNumSlowEnemies
                && Time.realtimeSinceStartup - _lastSpawnTime > _settings.MinDelayBetweenSpawns)
            {
                SpawnEnemy();
                _enemyCount++;
            }
        }

        void SpawnEnemy()
        {
            float value = Random.Range(0f, 1f);
            
            float speed;
            float hp;
            if (value <= 0.5)
            {
                speed = Random.Range(_settings.SpeedMinEnemy1, _settings.SpeedMaxEnemy1);
                hp = Random.Range(_settings.HPSMinEnemy1, _settings.HPMaxEnemy1);
            }
            else
            {
                speed = Random.Range(_settings.SpeedMinEnemy2, _settings.SpeedMaxEnemy2);
                hp = Random.Range(_settings.HPSMinEnemy2, _settings.HPMaxEnemy2);
            }

            GameObject gameObject;
            //Debug.LogError(value);
            if (value <= 0.5)
            {
                gameObject = _gameSettings.Enemy1FacadePrefab;
            }
            else
            {
                gameObject = _gameSettings.Enemy2FacadePrefab;
            }

            Vector3 position = ChooseRandomStartPosition();
            EnemyFacade enemyFacade = _enemyFactory.Create(hp, speed);
            enemyFacade.Position = position;

            _lastSpawnTime = Time.realtimeSinceStartup;
        }

        Vector3 ChooseRandomStartPosition()
        {
            // Define the distance from the camera where objects will be instantiated
            float zDistance = 0; // Place objects at this distance in front of the camera

            // Get frustum boundaries at that distance
            Vector3 bottomLeft =
                _camera.ViewportToWorldPoint(new Vector3(0, 0, zDistance)); // Bottom-left of the screen
            Vector3 topRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, zDistance)); // Top-right of the screen

            // Randomly pick one of four sides: 0 = left, 1 = right, 2 = top, 3 = bottom
            int side = Random.Range(0, 4);

            float xPosition = 0f, yPosition = 0f;

            switch (side)
            {
                case 0: // Left
                    xPosition = bottomLeft.x - 1f; // Slightly outside the left edge
                    yPosition = Random.Range(bottomLeft.y, topRight.y);
                    break;
                case 1: // Right
                    xPosition = topRight.x + 1f; // Slightly outside the right edge
                    yPosition = Random.Range(bottomLeft.y, topRight.y);
                    break;
                case 2: // Top
                    xPosition = Random.Range(bottomLeft.x, topRight.x);
                    yPosition = topRight.y + 1f; // Slightly outside the top edge
                    break;
                case 3: // Bottom
                    xPosition = Random.Range(bottomLeft.x, topRight.x);
                    yPosition = bottomLeft.y - 1f; // Slightly outside the bottom edge
                    break;
            }

            // Return the calculated off-screen position with the specified zDistance
            return new Vector3(xPosition, yPosition + 1, 0);
        }

        [Serializable]
        public class Settings
        {
            public float SpeedMinEnemy1;
            public float SpeedMaxEnemy1;

            public float HPSMinEnemy1;
            public float HPMaxEnemy1;
            
            public float SpeedMinEnemy2;
            public float SpeedMaxEnemy2;

            public float HPSMinEnemy2;
            public float HPMaxEnemy2;

            public float NumEnemiesIncreaseRate;
            public float NumEnemiesSlowStartAmount;
            public float NumEnemies2StartAmount;

            public float MinDelayBetweenSpawns = 0.5f;
        }
    }
}
