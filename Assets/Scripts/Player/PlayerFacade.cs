using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerFacade : MonoBehaviour
    {
        public PlayerModel _model;
        //PlayerDamageHandler _hitHandler;

        [Inject]
        public void Construct(PlayerModel player)
        {
            _model = player;
        }

        public bool IsDead
        {
            get { return _model.IsDead; }
        }

        public Vector3 Position
        {
            get { return _model.Position; }
        }

        public void Flip(bool flip)
        {
            _model.Self.flipX = flip;
            _model.Pistol.flipY = flip;
        }

        public void TakeDamage(Vector3 moveDirection)
        {
            //_hitHandler.TakeDamage(moveDirection);
        }
    }
}