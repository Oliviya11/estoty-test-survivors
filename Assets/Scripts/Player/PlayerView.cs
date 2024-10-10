using Misc;
using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _self;
        [SerializeField] PlayerAnimator _playerAnimator;
        [SerializeField] SpriteRenderer _pistol;
        [SerializeField] PingPongColor _pingPongColor;
        
        public PlayerAnimator PlayerAnimator => _playerAnimator;
        
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

        public void LaunchPingPongColor()
        {
            _pingPongColor.Launch();
        }
    }
}