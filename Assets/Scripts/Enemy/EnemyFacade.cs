using System;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyFacade : MonoBehaviour, IPoolable<float, float, IMemoryPool>, IDisposable
    {
        EnemyView _view;
        EnemyTunables _tunables;
        EnemyRegistry _registry;
        IMemoryPool _pool;
        
        public Vector3 Position
        {
            set { _view.Position = value; }
        }
        
        [Inject]
        public void Construct(
            EnemyView view,
            EnemyTunables tunables,
            EnemyRegistry registry)
        {
            _view = view;
            _tunables = tunables;
            _registry = registry;
        }

        public void OnDespawned()
        {
            _registry.RemoveEnemy(this);
            _pool = null;
        }

        public void OnSpawned(float maxHp, float speed, IMemoryPool pool)
        {
            _pool = pool;
            _tunables.MaxHP = maxHp;
            _tunables.Speed = speed;

            _registry.AddEnemy(this);
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }
        
        public class Factory : PlaceholderFactory<float, float, EnemyFacade>
        {
        }
    }
}