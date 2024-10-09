using System;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.States
{
    public class EnemyStateFollow : IEnemyState
    {
        readonly EnemyCommonSettings _commonSettings;
        readonly Settings _settings;
        readonly EnemyTunables _tunables;
        readonly EnemyStateManager _stateManager;
        readonly EnemyView _view;
        readonly PlayerFacade _player;

        public EnemyStateFollow(
            PlayerFacade player,
            EnemyView view,
            EnemyStateManager stateManager,
            EnemyTunables tunables,
            Settings settings,
            EnemyCommonSettings commonSettings)
        {
            _commonSettings = commonSettings;
            _settings = settings;
            _tunables = tunables;
            _stateManager = stateManager;
            _view = view;
            _player = player;
        }

        public void EnterState()
        {
            
        }

        public void ExitState()
        {
        }

        public void Update()
        {
            if (_player.IsDead || _view.Facade.IsDead)
            {
                _stateManager.ChangeState(EnemyStates.Idle);
                return;
            }

            var distanceToPlayer = (_player.Position - _view.Position).magnitude;

            if (distanceToPlayer < _commonSettings.AttackDistance)
            {
                _stateManager.ChangeState(EnemyStates.Attack);
            }
        }

        public void FixedUpdate()
        {
            MoveTowardsPlayer();
        }

        public void LateUpdate()
        {
            
        }

        void MoveTowardsPlayer()
        {
            var playerDir = (_player.Position - _view.Position).normalized;
            _view.Rigidbody.AddForce(playerDir * _tunables.Speed);
            FlipX();
        }

        void FlipX()
        {
            if (_view.Position.x < _player.Position.x)
            {
                _view.Facade.FlipX(false);
            }
            else
            {
                _view.Facade.FlipX(true);
            }
        }

        [Serializable]
        public class Settings
        {
            public float StrafeMultiplier;
            public float StrafeChangeInterval;
            public float TeleportNewDistance;
        }
    }
}