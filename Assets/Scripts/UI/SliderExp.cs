using System;
using Installers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SliderExp : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] Text levelText;
        [SerializeField] Text killText;
        [SerializeField] Slider expSlider;
        const string Level = "Lv .1";
        const string ZeroEnemies = "0";
        SignalBus _signalBus;
        GameInstaller.Settings _settings;

        [Inject]
        public void Constract(SignalBus signalBus, GameInstaller.Settings settings)
        {
            _signalBus = signalBus;
            _settings = settings;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<KillEnemySignal>(UpdateXP);
            levelText.text = Level;
            killText.text = ZeroEnemies;
            expSlider.value = 0;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<KillEnemySignal>(UpdateXP);
        }

        public void UpdateXP(KillEnemySignal signal)
        {
            expSlider.value = (signal.KilledEnemies % _settings.EnemyKillNumberToReachNextLevel) / (1f * _settings.EnemyKillNumberToReachNextLevel);
            killText.text = $"{signal.KilledEnemies}";
            levelText.text = $"Lv .{signal.KilledEnemies / _settings.EnemyKillNumberToReachNextLevel}";
        }
    }
}