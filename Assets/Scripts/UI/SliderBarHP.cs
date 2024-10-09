using System;
using Installers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SliderBarHP : MonoBehaviour, IInitializable, IDisposable
    {
        [SerializeField] Slider slider;
        SignalBus _signalBus;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            slider.value = 1;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerGotDamageSignal>(UpdateSlider);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerGotDamageSignal>(UpdateSlider);
        }
        
        void UpdateSlider(PlayerGotDamageSignal args)
        {
            slider.value = args.Value;
        }
    }
}