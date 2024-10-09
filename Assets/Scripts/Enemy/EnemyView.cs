using UI;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidBody;
        [SerializeField] EnemyAnimator _enemyAnimator;
        [SerializeField] HPBar _hpBar;
        [SerializeField] SpriteRenderer _spriteRenderer;
        
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
        
        public Rigidbody Rigidbody => _rigidBody;
        public EnemyAnimator EnemyAnimator => _enemyAnimator;
        public HPBar HpBar => _hpBar;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
    }
}