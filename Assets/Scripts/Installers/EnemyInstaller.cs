using Enemy;
using Enemy.States;
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
            Container.Bind<EnemyStateIdle>().AsSingle();
            Container.Bind<EnemyStateAttack>().AsSingle();
            Container.Bind<EnemyStateFollow>().AsSingle();
        }
    }
}