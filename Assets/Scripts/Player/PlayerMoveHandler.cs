using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMoveHandler : IFixedTickable
    {
        readonly Settings _settings;
        readonly PlayerModel _player;
        readonly PlayerInputState _inputState;

        public PlayerMoveHandler(
            PlayerInputState inputState,
            PlayerModel player,
            Settings settings)
        {
            _settings = settings;
            _player = player;
            _inputState = inputState;
        }

        public void FixedTick()
        {
            if (_player.IsDead) return;
            
            if (_inputState.IsMovingLeft)
            {
                _player.AddForce(
                    Vector3.left * _settings.MoveSpeed);
            }

            if (_inputState.IsMovingRight)
            {
                _player.AddForce(
                    Vector3.right * _settings.MoveSpeed);
            }

            if (_inputState.IsMovingUp)
            {
                _player.AddForce(
                    Vector3.up * _settings.MoveSpeed);
            }

            if (_inputState.IsMovingDown)
            {
                _player.AddForce(
                    Vector3.down * _settings.MoveSpeed);
            }

            if (_player.IsRunning())
            {
                _player.PlayerAnimator.PlayRun();
            }
            else
            {
                _player.PlayerAnimator.PlayIdle();
            }

            ClampPlayerPosition();
        }

        void ClampPlayerPosition()
        {
            Vector3 playerPosition = _player.Transform.position;
            if (_player.Transform.position.x < _settings.leftBoundary)
            {
                playerPosition.x = _settings.leftBoundary;
            }
            else if (_player.Transform.position.x > _settings.rightBoundary)
            {
                playerPosition.x = _settings.rightBoundary;
            }
            else if (_player.Transform.position.y > _settings.upBoundary)
            {
                playerPosition.y = _settings.upBoundary;
            }
            else if (_player.Transform.position.y < _settings.downBoundary)
            {
                playerPosition.y = _settings.downBoundary;
            }

            _player.Transform.position = playerPosition;
        }

        [Serializable]
        public class Settings
        {
            public float MoveSpeed;
            public float leftBoundary;
            public float rightBoundary;
            public float upBoundary;
            public float downBoundary;
        }
    }
}