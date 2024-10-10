using System.Collections;
using Installers;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerHealthWatcher : ITickable
    {
        readonly SignalBus _signalBus;
        readonly PlayerModel _player;

        public PlayerHealthWatcher(PlayerModel player, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _player = player;
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
            _player.PlayerAnimator.PlayDeath();
            _player.PlayerAnimator.StartCoroutine(FireDie());
        }

        IEnumerator FireDie()
        {
            yield return new WaitForSeconds(2);
            _signalBus.Fire<PlayerDiedSignal>();
        }
    }
}