using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public class CameraFollow : ILateTickable
    {
        readonly Settings _settings;
        readonly Transform _following;
        readonly Camera _camera;
        

        public CameraFollow(
            PlayerModel player,
            Settings settings, 
            Camera camera)
        {
            _following = player.Transform;
            _settings = settings;
            _camera = camera;
        }
        
        public void LateTick()
        {
            Quaternion rotation = Quaternion.Euler(_settings.RotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_settings.Distance) + FollowingPointPosition();

            _camera.transform.rotation = rotation;
            _camera.transform.position = position;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += _settings.OffsetY;

            return followingPosition;
        }
        
        [Serializable]
        public class Settings
        {
            public float RotationAngleX;
            public float Distance;
            public float OffsetY;
        }
    }
}