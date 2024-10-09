using System;
using UnityEngine;

namespace Player
{
    public class PlayerDamageHandler
    {
        //readonly AudioPlayer _audioPlayer;
        readonly Settings _settings;
        readonly PlayerModel _player;

        public PlayerDamageHandler(
            PlayerModel player,
            Settings settings
            //AudioPlayer audioPlayer
            )
        {
            //_audioPlayer = audioPlayer;
            _settings = settings;
            _player = player;
        }
        
        public void TakeDamage()
        {
            _player.TakeDamage(_settings.HealthLoss);
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