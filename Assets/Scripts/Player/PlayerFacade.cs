using Misc;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerFacade : MonoBehaviour
    {
        public PlayerModel _model;
        PlayerDamageHandler _hitHandler;

        [Inject]
        public void Construct(PlayerModel player, PlayerDamageHandler hitHandler)
        {
            _model = player;
            _hitHandler = hitHandler;
        }

        public bool IsDead => _model.IsDead;

        public Vector3 Position => _model.Position;

        public void TakeDamage()
        {
            _hitHandler.TakeDamage();
        }
    }
}