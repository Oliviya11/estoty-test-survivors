using System;
using Installers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Misc
{
    public class GameRestartHandler : IInitializable, IDisposable, ITickable
    {
        const string BootScene = "Boot";
        const string GameplayScene = "Gameplay";
        readonly SignalBus _signalBus;
        readonly Settings _settings;

        bool _isDelaying;
        float _delayStartTime;

        public GameRestartHandler(
            Settings settings,
            SignalBus signalBus)
        {
            _signalBus = signalBus;
            _settings = settings;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
        }
        
        public void Tick()
        {
            if (_isDelaying)
            {
                if (Time.realtimeSinceStartup - _delayStartTime > _settings.RestartDelay)
                {
                    SceneManager.UnloadSceneAsync(BootScene);
                    Resources.UnloadUnusedAssets();
                    SceneManager.LoadScene(GameplayScene);
                }
            }
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDied);
        }
        
        void OnPlayerDied()
        {
            _delayStartTime = Time.realtimeSinceStartup;
            _isDelaying = true;
            SceneManager.LoadScene(BootScene, LoadSceneMode.Additive);
        }
        
        [Serializable]
        public class Settings
        {
            public float RestartDelay;
        }
    }
}