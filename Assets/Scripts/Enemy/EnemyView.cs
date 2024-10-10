using System.Collections.Generic;
using Player;
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
        [SerializeField] EnemyChildView _slowEnemy;
        [SerializeField] EnemyChildView _fastEnemy;

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

        public void SetSlowEnemy()
        {
            SetGameObjects(_fastEnemy, _slowEnemy);
        }

        public void SetFastEnemy()
        {
            SetGameObjects(_slowEnemy, _fastEnemy);
        }

        void SetGameObjects(EnemyChildView inactive, EnemyChildView active)
        {
            inactive.gameObject.SetActive(false);
            active.gameObject.SetActive(true);
            _enemyAnimator._animator = active.Animator;
            _spriteRenderer = active.SpriteRenderer;
        }
    }
}