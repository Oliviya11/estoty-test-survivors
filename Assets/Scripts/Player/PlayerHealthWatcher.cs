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
        readonly PlayerModel _playerModel;
        readonly PlayerView _playerView;
        readonly AudioPlayer _audioPlayer;
        readonly GameInstaller.Settings _settings;

        public PlayerHealthWatcher(PlayerModel playerModel, PlayerView playerView, SignalBus signalBus, 
            AudioPlayer audioPlayer, GameInstaller.Settings settings)
        {
            _signalBus = signalBus;
            _playerModel = playerModel;
            _playerView = playerView;
            _audioPlayer = audioPlayer;
            _settings = settings;
        }

        public void Tick()
        {
            if (_playerModel.CurrentHealth <= 0 && !_playerModel.IsDead)
            {
               Die();
            }
        }

        void Die()
        {
            _playerModel.IsDead = true;
            _playerView.PlayDeath();
            _audioPlayer.Play(_settings.LoseClip, _settings.LoseVolume);
            _playerView.PlayerAnimator.StartCoroutine(FireDie());
        }

        IEnumerator FireDie()
        {
            yield return new WaitForSeconds(2);
            _signalBus.Fire<PlayerDiedSignal>();
        }
    }
}