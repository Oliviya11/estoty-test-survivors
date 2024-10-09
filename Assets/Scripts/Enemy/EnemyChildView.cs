using Enemy;
using UI;
using UnityEngine;

namespace Player
{
    public class EnemyChildView : MonoBehaviour
    {
        public Rigidbody _rigidBody;
        public EnemyAnimator _enemyAnimator;
        public HPBar _hpBar;
        public SpriteRenderer _spriteRenderer;
    }
}