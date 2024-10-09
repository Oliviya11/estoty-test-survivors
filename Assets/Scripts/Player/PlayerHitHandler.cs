using System;
using UnityEngine;

namespace Player
{
    public class PlayerHitHandler
    {
        //readonly AudioPlayer _audioPlayer;
        readonly Settings _settings;
        readonly PlayerModel _player;

        public PlayerHitHandler(
            PlayerModel player,
            Settings settings
            //AudioPlayer audioPlayer
            )
        {
            //_audioPlayer = audioPlayer;
            _settings = settings;
            _player = player;
        }
        
        [Serializable]
        public class Settings
        {
            public float HealthLoss;
            public float HitForce;

            public AudioClip HitSound;
            public float HitSoundVolume = 1.0f;
        }
    }
}