using System;
using Misc;
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
                .WithArguments(_settings.Rigidbody, _settings.MaxHealth, _settings.Pistol, _settings.Self,
                    _settings.PingPongColor, _settings.PlayerAnimator);
            
            Container.BindInterfacesTo<PlayerInputHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();
            Container.BindInterfacesTo<PlayerShootHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDamageHandler>().AsSingle();
            Container.BindInterfacesTo<CameraFollow>().AsSingle();
            Container.Bind<PlayerInputState>().AsSingle();
            Container.BindInterfacesTo<PlayerHealthWatcher>().AsSingle();
        }
        
        [Serializable]
        public class Settings
        {
            public Rigidbody Rigidbody;
            public float MaxHealth;
            public SpriteRenderer Pistol;
            public SpriteRenderer Self;
            public PingPongColor PingPongColor;
            public PlayerAnimator PlayerAnimator;
        }
    }
}