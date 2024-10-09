using Enemy;
using Enemy.States;
using Misc;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyTunables>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyStateManager>().AsSingle();
            Container.Bind<EnemyStateNone>().AsSingle();
            Container.Bind<EnemyStateAttack>().AsSingle();
            Container.Bind<EnemyStateFollow>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyHitDeathHandler>().AsSingle();
        }
    }
}