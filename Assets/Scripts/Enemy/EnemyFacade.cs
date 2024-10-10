using System;
using Player;
using UI;
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
        EnemyHitDeathHandler _enemyHitDeathHandler;
        EnemyStateManager _enemyStateManager;

        float health;
        public float Health
        {
            get => health;
            set => health = value;
        }

        public float MaxHP => _tunables.MaxHP;

        public Vector3 Position
        {
            set { _view.Position = value; }
        }

        public HPBar HpBar => _view.HpBar;
        
        public bool IsDead
        {
            get; set;
        }

        [Inject]
        public void Construct(
            EnemyView view,
            EnemyTunables tunables,
            EnemyRegistry registry,
            EnemyHitDeathHandler enemyHitDeathHandler,
            EnemyStateManager enemyStateManager)
        {
            _view = view;
            _tunables = tunables;
            _registry = registry;
            _enemyHitDeathHandler = enemyHitDeathHandler;
            _enemyStateManager = enemyStateManager;
        }

        public void OnDespawned()
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            _registry.RemoveEnemy(this);
            _pool = null;
        }

        public void OnSpawned(float maxHp, float speed, IMemoryPool pool)
        {
            _pool = pool;
            _tunables.MaxHP = maxHp;
            _tunables.Speed = speed;
            Health = maxHp;
            _view.EnemyAnimator.Reset();
            _view.Facade.IsDead = false;
            _view.HpBar.SetValue(_view.Facade.Health, _view.Facade.MaxHP);
            _enemyStateManager.ChangeState(EnemyStates.Follow);
            _registry.AddEnemy(this);
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }
        
        public void AddForce(Vector3 force)
        {
            _view.Rigidbody.AddForce(force);
        }

        public void PlayDeath()
        {
            _view.EnemyAnimator.PlayDeath();
        }

        public void PlayHit()
        {
            _view.EnemyAnimator.PlayHit();
        }

        public void Hit(float damage)
        {
            _enemyHitDeathHandler.Hit(damage);
        }

        public void Stop()
        {
            _view.Rigidbody.velocity = Vector3.zero;
        }

        public void FlipX(bool value)
        {
            _view.SpriteRenderer.flipX = value;
        }

        public class Factory : PlaceholderFactory<float, float, EnemyFacade>
        {
        }
    }
}