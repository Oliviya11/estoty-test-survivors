using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidBody;
        [Inject]
        public EnemyFacade Facade
        {
            get; set;
        }
        
        public Vector3 Position
        {
            get { return _rigidBody.transform.position; }
            set { _rigidBody.transform.position = value; }
        }
        
        public void AddForce(Vector3 force)
        {
            _rigidBody.AddForce(force);
        }

        public void Stop()
        {
            _rigidBody.velocity = Vector3.zero;
        }
        public Vector3 RightDir
        {
            get { return _rigidBody.transform.up; }
        }

        public Vector3 ForwardDir
        {
            get { return _rigidBody.transform.right; }
        }
    }
}