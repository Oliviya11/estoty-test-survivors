using System;
using Installers;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerDamageHandler
    {
        //readonly AudioPlayer _audioPlayer;
        readonly Settings _settings;
        readonly PlayerModel _player;
        readonly SignalBus _signalBus;

        public PlayerDamageHandler(
            PlayerModel player,
            Settings settings,
            SignalBus signalBus
            //AudioPlayer audioPlayer
            )
        {
            //_audioPlayer = audioPlayer;
            _settings = settings;
            _player = player;
            _signalBus = signalBus;
        }
        
        public void TakeDamage()
        {
            _player.TakeDamage(_settings.HealthLoss);
            _signalBus.Fire(new PlayerGotDamageSignal(_player.CurrentHealth/_player.MaxHealth));
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