using System;
using Installers;
using Misc;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerDamageHandler
    {
        readonly Settings _settings;
        readonly PlayerModel _playerModel;
        readonly PlayerView _playerView;
        readonly SignalBus _signalBus;
        readonly AudioPlayer _audioPlayer;

        public PlayerDamageHandler(
            PlayerModel playerModel,
            PlayerView playerView,
            Settings settings,
            SignalBus signalBus,
            AudioPlayer audioPlayer
            )
        {
            _settings = settings;
            _playerModel = playerModel;
            _playerView = playerView;
            _signalBus = signalBus;
            _audioPlayer = audioPlayer;
        }
        
        public void TakeDamage()
        {
            _playerModel.TakeDamage(_settings.HealthLoss);
            _playerView.LaunchPingPongColor();
            _signalBus.Fire(new PlayerGotDamageSignal(_playerModel.CurrentHealth/_playerModel.MaxHealth));
            _audioPlayer.Play(_settings.HitSound, _settings.HitSoundVolume);
        }
        
        [Serializable]
        public class Settings
        {
            public float HealthLoss;

            public AudioClip HitSound;
            public float HitSoundVolume = 1.0f;
        }
    }
}