using System;
using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Survivor's-Like Shooter/Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public PlayerSettings Player;
        
        [Serializable]
        public class PlayerSettings
        {
            public PlayerMoveHandler.Settings PlayerMoveHandler;
            public CameraFollow.Settings CameraFollow;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(Player.PlayerMoveHandler).IfNotBound();
            Container.BindInstance(Player.CameraFollow).IfNotBound();
        }
    }
}