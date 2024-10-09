using Enemy;
using UnityEngine;
using Zenject;

namespace Misc
{
    public class Bullet : MonoBehaviour, IPoolable<float, float, float, IMemoryPool>
    {
        IMemoryPool _pool;
        float _speed;
        float _lifeTime;
        float _startTime;
        float _damage;
        
        public void Update()
        {
            transform.position += transform.right * _speed * Time.deltaTime;

            if (Time.realtimeSinceStartup - _startTime > _lifeTime)
            {
                _pool.Despawn(this);
            }
        }
        
        public void OnTriggerEnter(Collider other)
        {
            var enemyView = other.GetComponent<EnemyView>();

            if (enemyView != null)
            {
                enemyView.Facade.Hit(_damage);
                _pool.Despawn(this);
            }
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void OnSpawned(float speed, float lifeTime, float damage, IMemoryPool pool)
        {
            _pool = pool;
            _speed = speed;
            _lifeTime = lifeTime;
            _damage = damage;

            _startTime = Time.realtimeSinceStartup;
        }
        
        public class Factory : PlaceholderFactory<float, float, float, Bullet>
        {
        }
    }
}