using Enemy;
using Player;
using System.Collections;
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
        Coroutine despawnCoroutine;


        public void Update()
        {
            transform.position += transform.right * _speed * Time.deltaTime;

            if (Time.realtimeSinceStartup - _startTime > _lifeTime)
            {
                _pool.Despawn(this);
            }
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            var enemyView = other.transform.GetComponent<EnemyView>();
            
            if (enemyView != null)
            {
                enemyView.Facade.Hit(_damage);
                if (despawnCoroutine == null)
                    despawnCoroutine = StartCoroutine(DespawnAfterFrame());
            }
        }

        IEnumerator DespawnAfterFrame()
        {
            yield return new WaitForEndOfFrame(); // Wait until the end of the current frame
            _pool.Despawn(this); // Now despawn it
            despawnCoroutine = null;
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