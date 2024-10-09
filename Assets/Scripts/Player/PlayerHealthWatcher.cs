using Installers;
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
            _signalBus.Fire<PlayerDiedSignal>();
        }
    }
}