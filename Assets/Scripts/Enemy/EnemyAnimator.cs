using UnityEditor.Animations;
using UnityEngine;

namespace Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] public Animator _animator;
        static readonly int HitHash = Animator.StringToHash("Hit");
        static readonly string Dead = "Dead";
        
        public void PlayHit()
        {
            _animator.SetTrigger(HitHash);
        }

        public void PlayDeath()
        {
            _animator.SetBool(Dead, true);
        }
        
    }
}