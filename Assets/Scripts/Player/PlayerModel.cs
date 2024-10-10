using Misc;
using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        const float Epsilon = 0.01f;
        public SpriteRenderer Self;
        public readonly PlayerAnimator PlayerAnimator;
        public readonly SpriteRenderer Pistol;
        public readonly PingPongColor PingPongColor;
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
            Pistol = pistol;
            Self = self;
            PingPongColor = pingPongColor;
            PlayerAnimator = playerAnimator;
        }
        
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
        }
    }
}