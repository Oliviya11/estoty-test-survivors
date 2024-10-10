using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] public Animator _animator;
        static readonly string Dead = "Dead";
        static readonly string Run = "Run";
        bool isRun;
        bool isIdle;

        public void PlayIdle()
        {
            isRun = false;
            if (isIdle) return;
            isIdle = true;
            _animator.SetBool(Run, false);
        }

        public void PlayRun()
        {
            isIdle = false;
            if (isRun) return;
            isRun = true;
            _animator.SetBool(Run, true);
        }

        public void PlayDeath()
        {
            _animator.SetBool(Dead, true);
        }
        
        public void Reset()
        {
            isRun = false;
            isIdle = false;
            _animator.SetBool(Run, false);
            _animator.SetBool(Dead, false);
        }
    }
}