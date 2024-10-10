using System;
using Misc;
using Player;
using UnityEngine;

namespace Enemy.States
{
    public class EnemyStateAttack : IEnemyState
    {
        readonly EnemyCommonSettings _commonSettings;
        readonly EnemyTunables _tunables;
        readonly EnemyStateManager _stateManager;
        readonly PlayerFacade _player;
        readonly Settings _settings;
        readonly EnemyView _view;
        readonly AudioPlayer _audioPlayer;
        float _lastHitTime;

        public EnemyStateAttack(
            PlayerFacade player,
            EnemyView view,
            EnemyStateManager stateManager,
            EnemyTunables tunables,
            Settings settings,
            EnemyCommonSettings commonSettings,
            AudioPlayer audioPlayer)
        {
            _commonSettings = commonSettings;
            _settings = settings;
            _tunables = tunables;
            _stateManager = stateManager;
            _view = view;
            _player = player;
            _audioPlayer = audioPlayer;
        }

        public void EnterState()
        {
            //throw new System.NotImplementedException();
        }

        public void ExitState()
        {
            //throw new System.NotImplementedException();
        }

        public void Update()
        {
            if (_player.IsDead)
            {
                return;
            }
            
            if (Time.realtimeSinceStartup - _lastHitTime > _settings.HitInterval)
            {
                _lastHitTime = Time.realtimeSinceStartup;
                HitPlayer();
            }
            
            if ((_player.Position - _view.Position).magnitude > -_commonSettings.AttackDistance)
            {
                _stateManager.ChangeState(EnemyStates.Follow);
            }
        }

        void HitPlayer()
        {
            _player.PingPongColor.Launch();
            _player.TakeDamage();
            _audioPlayer.Play(_settings.HitSound, _settings.HitVolume);
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
            public AudioClip HitSound;
            public float HitVolume;
            public float HitInterval;
        }
    }
}