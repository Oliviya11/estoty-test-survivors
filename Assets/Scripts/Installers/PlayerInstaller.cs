using System;
using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        Settings _settings = null;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerModel>().AsSingle()
                .WithArguments(_settings.Rigidbody, _settings.MaxHealth);
            
            Container.BindInterfacesTo<PlayerInputHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();
            Container.Bind<PlayerInputState>().AsSingle();
        }
        
        [Serializable]
        public class Settings
        {
            public Rigidbody Rigidbody;
            public float MaxHealth;
        }
    }
}