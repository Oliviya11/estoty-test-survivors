using System;
using Enemy;
using Misc;
using UI;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Inject]
        Settings _settings = null;
        
        public override void InstallBindings()
        {
            Container.Bind<Progress>().AsSingle();
            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindFactory<float, float, EnemyFacade, EnemyFacade.Factory>()
                // We could just use FromMonoPoolableMemoryPool here instead, but
                // for IL2CPP to work we need our pool class to be used explicitly here
                .FromPoolableMemoryPool<float, float, EnemyFacade, EnemyFacadePool>(poolBinder => poolBinder
                    // Spawn 5 enemies right off the bat so that we don't incur spikes at runtime
                    .WithInitialSize(5)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<EnemyInstaller>(_settings.EnemyHolderPrefab)
                    // Place each enemy under an Enemies game object at the root of scene hierarchy
                    .UnderTransformGroup("Enemies"));
            
            Container.BindFactory<float, float, float, Bullet, Bullet.Factory>()
                // We could just use FromMonoPoolableMemoryPool here instead, but
                // for IL2CPP to work we need our pool class to be used explicitly here
                .FromPoolableMemoryPool<float, float, float, Bullet, BulletPool>(poolBinder => poolBinder
                    // Spawn 20 right off the bat so that we don't incur spikes at runtime
                    .WithInitialSize(20)
                    // Bullets are simple enough that we don't need to make a subcontainer for them
                    // The logic can all just be in one class
                    .FromComponentInNewPrefab(_settings.BulletPrefab)
                    .UnderTransformGroup("Bullets"));
            
            Container.Bind<EnemyRegistry>().AsSingle();
            
            Container.BindInterfacesTo<GameRestartHandler>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<SliderBarHP>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<SliderExp>().FromComponentInHierarchy().AsSingle();
            
            GameSignalsInstaller.Install(Container);
        }
        
        class EnemyFacadePool : MonoPoolableMemoryPool<float, float, IMemoryPool, EnemyFacade>
        {
        }
        
        class BulletPool : MonoPoolableMemoryPool<float, float, float, IMemoryPool, Bullet>
        {
        }

        [Serializable]
        public class Settings
        {
            public GameObject EnemyHolderPrefab;
            public GameObject Enemy1FacadePrefab;
            public GameObject Enemy2FacadePrefab;
            public GameObject BulletPrefab;
            public int EnemyKillNumberToReachNextLevel;
        }
    }
}