using System;
using Installers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SliderBarHP : IInitializable, IDisposable
    {
        SignalBus _signalBus;
        SliderBarHPView _view;
        
        [Inject]
        public void Construct(SliderBarHPView view, SignalBus signalBus)
        {
            _view = view;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _view.slider.value = 1;
            _signalBus.Subscribe<PlayerGotDamageSignal>(UpdateSlider);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerGotDamageSignal>(UpdateSlider);
        }
        
        void UpdateSlider(PlayerGotDamageSignal args)
        {
            _view.slider.value = args.Value;
        }
    }
}