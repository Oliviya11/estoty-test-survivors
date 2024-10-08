using System;
using UnityEngine;
using Zenject;
namespace Player
{
    public class PlayerInputHandler : ITickable
    {
        const string Horizontal = "Horizontal";
        const string Vertical = "Vertical";
        readonly PlayerInputState _inputState;

        public PlayerInputHandler(PlayerInputState inputState)
        {
            _inputState = inputState;
        }
        
        public void Tick()
        {
            float horizontalInput = SimpleInput.GetAxis(Horizontal);
            _inputState.IsMovingLeft = horizontalInput < 0;
            _inputState.IsMovingRight = horizontalInput > 0;
            float verticalInput = SimpleInput.GetAxis(Vertical);
            _inputState.IsMovingUp = verticalInput > 0;
            _inputState.IsMovingDown = verticalInput < 0;
            _inputState.IsStopped = Math.Abs(verticalInput) < float.Epsilon && Math.Abs(horizontalInput) < float.Epsilon;
        }
    }
}