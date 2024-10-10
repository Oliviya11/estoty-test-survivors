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
        readonly PlayerModel _player;
        readonly SignalBus _signalBus;
        readonly AudioPlayer _audioPlayer;

        public PlayerDamageHandler(
            PlayerModel player,
            Settings settings,
            SignalBus signalBus,
            AudioPlayer audioPlayer
            )
        {
            _settings = settings;
            _player = player;
            _signalBus = signalBus;
            _audioPlayer = audioPlayer;
        }
        
        public void TakeDamage()
        {
            _player.TakeDamage(_settings.HealthLoss);
            _signalBus.Fire(new PlayerGotDamageSignal(_player.CurrentHealth/_player.MaxHealth));
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