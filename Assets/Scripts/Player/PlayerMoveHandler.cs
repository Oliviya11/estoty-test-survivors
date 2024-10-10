using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMoveHandler : IFixedTickable
    {
        readonly Settings _settings;
        readonly PlayerModel _playerModel;
        readonly PlayerView _playerView;
        readonly PlayerInputState _inputState;

        public PlayerMoveHandler(
            PlayerInputState inputState,
            PlayerModel playerModel,
            PlayerView playerView,
            Settings settings)
        {
            _settings = settings;
            _playerModel = playerModel;
            _playerView = playerView;
            _inputState = inputState;
        }

        public void FixedTick()
        {
            if (_playerModel.IsDead) return;
            
            if (_inputState.IsMovingLeft)
            {
                _playerModel.AddForce(
                    Vector3.left * _settings.MoveSpeed);
            }

            if (_inputState.IsMovingRight)
            {
                _playerModel.AddForce(
                    Vector3.right * _settings.MoveSpeed);
            }

            if (_inputState.IsMovingUp)
            {
                _playerModel.AddForce(
                    Vector3.up * _settings.MoveSpeed);
            }

            if (_inputState.IsMovingDown)
            {
                _playerModel.AddForce(
                    Vector3.down * _settings.MoveSpeed);
            }

            if (_playerModel.IsRunning())
            {
                _playerView.PlayRun();
            }
            else
            {
                _playerView.PlayIdle();
            }

            ClampPlayerPosition();
        }

        void ClampPlayerPosition()
        {
            Vector3 playerPosition = _playerModel.Transform.position;
            if (_playerModel.Transform.position.x < _settings.leftBoundary)
            {
                playerPosition.x = _settings.leftBoundary;
            }
            else if (_playerModel.Transform.position.x > _settings.rightBoundary)
            {
                playerPosition.x = _settings.rightBoundary;
            }
            else if (_playerModel.Transform.position.y > _settings.upBoundary)
            {
                playerPosition.y = _settings.upBoundary;
            }
            else if (_playerModel.Transform.position.y < _settings.downBoundary)
            {
                playerPosition.y = _settings.downBoundary;
            }

            _playerModel.Transform.position = playerPosition;
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