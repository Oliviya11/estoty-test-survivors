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

        public bool IsDead
        {
            get { return _model.IsDead; }
        }

        public Vector3 Position
        {
            get { return _model.Position; }
        }

        public PingPongColor PingPongColor => _model.PingPongColor;

        public void FlipXPlayer(bool flip)
        {
            _model.Self.flipX = flip;
        }

        public void FlipYPistol(bool flip)
        {
            _model.Pistol.flipY = flip;
        }
        
        public void FlipXPistol(bool flip)
        {
            _model.Pistol.flipX = flip;
        }

        public void TakeDamage()
        {
            _hitHandler.TakeDamage();
        }
    }
}