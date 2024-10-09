using UI;
using Zenject;

namespace Installers
{
    public class GameSignalsInstaller : Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<PlayerDiedSignal>();
            Container.DeclareSignal<PlayerGotDamageSignal>();
            Container.DeclareSignal<KillEnemySignal>();
            
            //Container.BindSignal<PlayerGotDamageSignal>().ToMethod<SliderBar>(x => x.UpdateSlider).FromNew();
        }
    }
}