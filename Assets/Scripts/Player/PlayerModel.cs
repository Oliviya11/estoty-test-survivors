using Misc;
using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        const float Epsilon = 0.01f;
        SpriteRenderer _self;
        readonly PlayerAnimator _playerAnimator;
        readonly SpriteRenderer _pistol;
        readonly PingPongColor _pingPongColor;
        readonly Rigidbody _rigidBody;
        float _maxHealth;
        float _currentHealth;

        public PlayerModel(
            Rigidbody rigidBody,
            float maxHealth,
            SpriteRenderer pistol,
            SpriteRenderer self,
            PingPongColor pingPongColor,
            PlayerAnimator playerAnimator)
        {
            _rigidBody = rigidBody;
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
            _pistol = pistol;
            _self = self;
            _pingPongColor = pingPongColor;
            _playerAnimator = playerAnimator;
        }

        public PlayerAnimator PlayerAnimator => _playerAnimator;

        public Vector3 Position
        {
            get { return _rigidBody.position; }
            set { _rigidBody.position = value; }
        }

        public Transform Transform
        {
            get => _rigidBody.transform;
        }
        
        public void AddForce(Vector3 force)
        {
            _rigidBody.AddForce(force);
        }

        public bool IsRunning() => _rigidBody.velocity.sqrMagnitude >= Epsilon;

        public bool IsDead
        {
            get; set;
        }

        public float CurrentHealth => _currentHealth;

        public float MaxHealth => _maxHealth;
        
        public void TakeDamage(float healthLoss)
        {
            _currentHealth = Mathf.Max(0.0f, _currentHealth - healthLoss);
            _pingPongColor.Launch();
        }

        public void PlayRun()
        {
            _playerAnimator.PlayRun();
        }

        public void PlayIdle()
        {
            _playerAnimator.PlayIdle();
        }

        public void PlayDeath()
        {
            _playerAnimator.PlayDeath();
        }

        public void ChangePistolEuler(Vector3 angles)
        {
            _pistol.transform.eulerAngles = angles;
        }

        public void SetPistolQuaternion(Quaternion quaternion)
        {
            _pistol.transform.rotation = quaternion;
        }
        
        public void FlipXPlayer(bool flip)
        {
            _self.flipX = flip;
        }

        public void FlipYPistol(bool flip)
        {
            _pistol.flipY = flip;
        }
        
        public void FlipXPistol(bool flip)
        {
            _pistol.flipX = flip;
        }
    }
}