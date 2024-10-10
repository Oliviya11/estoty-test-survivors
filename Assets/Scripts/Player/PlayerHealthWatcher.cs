using System.Collections;
using Installers;
using Misc;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerHealthWatcher : ITickable
    {
        readonly SignalBus _signalBus;
        readonly PlayerModel _player;
        readonly AudioPlayer _audioPlayer;
        readonly GameInstaller.Settings _settings;

        public PlayerHealthWatcher(PlayerModel player, SignalBus signalBus, 
            AudioPlayer audioPlayer, GameInstaller.Settings settings)
        {
            _signalBus = signalBus;
            _player = player;
            _audioPlayer = audioPlayer;
            _settings = settings;
        }

        public void Tick()
        {
            if (_player.CurrentHealth <= 0 && !_player.IsDead)
            {
               Die();
            }
        }

        void Die()
        {
            _player.IsDead = true;
            _player.PlayDeath();
            _audioPlayer.Play(_settings.LoseClip, _settings.LoseVolume);
            _player.PlayerAnimator.StartCoroutine(FireDie());
        }

        IEnumerator FireDie()
        {
            yield return new WaitForSeconds(2);
            _signalBus.Fire<PlayerDiedSignal>();
        }
    }
}