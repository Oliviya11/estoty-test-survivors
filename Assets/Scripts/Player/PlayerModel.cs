using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        readonly Rigidbody _rigidBody;
        float _maxHealth;
        float _currentHealth;
        
        public PlayerModel(
            Rigidbody rigidBody,
            float maxHealth)
        {
            _rigidBody = rigidBody;
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
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

        public void Stop()
        {
            _rigidBody.velocity = Vector3.zero;
        }
        
        public bool IsDead
        {
            get; set;
        }
    }
}