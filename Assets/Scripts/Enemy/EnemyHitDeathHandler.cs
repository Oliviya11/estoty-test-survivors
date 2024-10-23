using System.Collections;
using Installers;
using Misc;
using Player;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

namespace Enemy
{
    public class EnemyHitDeathHandler
    {
        const float WaitBeforeDispose = 1f;
        readonly EnemyFacade _facade;
        readonly SignalBus _signalBus;
        //readonly Settings _settings;
        //readonly Explosion.Factory _explosionFactory;
        //readonly AudioPlayer _audioPlayer;
        readonly EnemyView _view;
        readonly Progress _progress;

        public EnemyHitDeathHandler(
            EnemyView view,
            //AudioPlayer audioPlayer,
            //Explosion.Factory explosionFactory,
            //Settings settings,
            SignalBus signalBus,
            EnemyFacade facade,
            Progress progress)
        {
            _facade = facade;
            _signalBus = signalBus;
            //_settings = settings;
            //_explosionFactory = explosionFactory;
            //_audioPlayer = audioPlayer;
            _view = view;
            _progress = progress;
        }

        public void Hit(float damage)
        {
            if (_view.Facade.IsDead) return;
            
                _view.Facade.Health -= damage;
            if (_view.Facade.Health <= 0)
            {
                _view.Facade.Stop();
                _view.Facade.PlayDeath();
                _view.Facade.IsDead = true;
                _view.Facade.StartCoroutine(Dispose());
                ++_progress.KilledEnemiesNumber;
                _signalBus.Fire(new KillEnemySignal(_progress.KilledEnemiesNumber));
            }
            else
            {
                _view.Facade.PlayHit();
            }
            _view.HpBar.SetValue(_view.Facade.Health, _view.Facade.MaxHP);
        }

        IEnumerator Dispose()
        {
            yield return new WaitForSeconds(WaitBeforeDispose);
            _view.Facade.Dispose();
        }
    }
}