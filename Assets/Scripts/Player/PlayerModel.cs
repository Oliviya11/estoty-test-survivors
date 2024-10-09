using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        public SpriteRenderer Self;
        public readonly SpriteRenderer Pistol;
        readonly Rigidbody _rigidBody;
        float _maxHealth;
        float _currentHealth;

        public PlayerModel(
            Rigidbody rigidBody,
            float maxHealth,
            SpriteRenderer pistol,
            SpriteRenderer self)
        {
            _rigidBody = rigidBody;
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
            Pistol = pistol;
            Self = self;
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

        public bool IsDead
        {
            get; set;
        }
    }
}