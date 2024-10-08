using System;
using Player;
using UnityEngine;

namespace Enemy.States
{
    public class EnemyStateAttack : IEnemyState
    {
        readonly EnemyCommonSettings _commonSettings;

        //readonly AudioPlayer _audioPlayer;
        readonly EnemyTunables _tunables;
        readonly EnemyStateManager _stateManager;
        readonly PlayerFacade _player;
        readonly Settings _settings;
        readonly EnemyView _view;

        public EnemyStateAttack(
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
            Debug.LogError("Enter Attack");
            //throw new System.NotImplementedException();
        }

        public void ExitState()
        {
            //throw new System.NotImplementedException();
        }

        public void Update()
        {
            if ((_player.Position - _view.Position).magnitude > _commonSettings.AttackDistance)
            {
                _stateManager.ChangeState(EnemyStates.Follow);
            }
        }

        public void FixedUpdate()
        {
            //throw new System.NotImplementedException();
        }

        public void LateUpdate()
        {
            //throw new System.NotImplementedException();
        }
        
        [Serializable]
        public class Settings
        {
            public AudioClip MeleeSound;
            public float AttackRadius;
        }
    }
}