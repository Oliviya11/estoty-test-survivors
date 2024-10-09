using System;
using Enemy;
using UnityEngine;
using Zenject;
using Bullet = Misc.Bullet;

namespace Player
{
    public class PlayerShootHandler : ITickable
    {
        const string Player = "Player";
        readonly PlayerFacade _player;
        readonly Settings _settings;
        readonly Bullet.Factory _bulletFactory;
        float _lastFireTime;
        private LayerMask layerMask;
        private Collider[] colliders = new Collider[1];
        int _layerMask;

        public PlayerShootHandler(
            Bullet.Factory bulletFactory,
            Settings settings,
            PlayerFacade player)
        {
            _player = player;
            _settings = settings;
            _bulletFactory = bulletFactory;
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }
        
        public void Tick()
        {
            if (_player.IsDead)
            {
                return;
            }

            if (Time.realtimeSinceStartup - _lastFireTime > _settings.MaxShootInterval)
            {
                _lastFireTime = Time.realtimeSinceStartup;
                Fire();
            }
        }

        void Fire()
        {
            Collider collider = GetClosestEnemy();
            if (collider != null)
            {
                // Get direction to the target, considering X and Y positions.
                var direction = DirectionToTarget(collider.transform);
                Quaternion quaternion = GetQuaternionToTarget(direction);
                RotatePlayer(collider.transform);
                RotatePistolTowardsEnemy(quaternion);
                LaunchBullet(direction, quaternion);
            }
        }

        void RotatePlayer(Transform target)
        {
            if (target.position.x < _player.Position.x)
            {
                _player.Flip(true);
            }
            else
            {
                _player.Flip(false);
            }
        }

        void RotatePistolTowardsEnemy(Quaternion quaternion)
        {
            // Apply the rotation to the object, only changing the Z axis (Euler angle).
            _player._model.Pistol.transform.rotation = quaternion;
        }

        Quaternion GetQuaternionToTarget(Vector3 direction)
        {
            // Calculate the angle in radians from the direction vector.
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, 0, angle);
        }

        Vector3 DirectionToTarget(Transform target)
        {
            Vector3 direction = target.position - _player.Position;
            return direction;
        }

        void LaunchBullet(Vector3 direction, Quaternion quaternion)
        {
            var bullet = _bulletFactory.Create(
                _settings.BulletSpeed, _settings.BulletLifetime, _settings.BulletDamage);

            bullet.transform.position = _player.Position + direction * _settings.BulletOffsetDistance;
            bullet.transform.rotation = quaternion;
        }

        Collider GetClosestEnemy()
        {
            // Get all colliders within the defined radius
            var size = Physics.OverlapSphereNonAlloc(_player.Position, _settings.Range, colliders, _layerMask);
        
            Collider closestCollider = null;
            float closestDistance = Mathf.Infinity;

            // Loop through each collider and find the closest one
            foreach (Collider collider in colliders)
            {
                if (collider == null) continue;
                float distance = Vector3.Distance(_player.Position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCollider = collider;
                }
            }

            return closestCollider;
        }


        [Serializable]
        public class Settings
        {
            public AudioClip BulletClip;
            public float LaserVolume = 1.0f;

            public float BulletLifetime;
            public float BulletSpeed;
            public float BulletDamage;
            public float BulletOffsetDistance;
            public float MaxShootInterval;
            public float Range;
        }
    }
}