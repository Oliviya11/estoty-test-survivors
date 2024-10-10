using System;
using Installers;
using Misc;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SliderExp : IInitializable, IDisposable
    {
        SliderExpView _view;
        const string Level = "Lv .1";
        const string ZeroEnemies = "0";
        SignalBus _signalBus;
        AudioPlayer _audioPlayer;
        GameInstaller.Settings _settings;

        [Inject]
        public void Constract(SliderExpView view, SignalBus signalBus, AudioPlayer audioPlayer, GameInstaller.Settings settings)
        {
            _view = view;
            _signalBus = signalBus;
            _audioPlayer = audioPlayer;
            _settings = settings;
        }

        public void Initialize()
        {
            _view.levelText.text = Level;
            _view.killText.text = ZeroEnemies;
            _view.expSlider.value = 0;
            _signalBus.Subscribe<KillEnemySignal>(UpdateXP);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<KillEnemySignal>(UpdateXP);
        }

        public void UpdateXP(KillEnemySignal signal)
        {
            int modLevel = signal.KilledEnemies % _settings.EnemyKillNumberToReachNextLevel;
            _view.expSlider.value = (modLevel) / (1f * _settings.EnemyKillNumberToReachNextLevel);
            _view.killText.text = $"{signal.KilledEnemies}";
            int level = signal.KilledEnemies / _settings.EnemyKillNumberToReachNextLevel;
            _view.levelText.text = $"Lv .{level}";

            if (level > 1 && modLevel == 0)
            {
                _audioPlayer.Play(_settings.LevelUpClip, _settings.LevelUpVolume);
            }
        }
    }
}