using System;
using Persistence;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Sliders
{
    public class UISliderController: MonoBehaviour
    {
        [SerializeField] private Settings settings;
        private Slider slider;
        private string name;

        private void Start()
        {
            slider = GetComponent<Slider>();
            InitSlider();
            
            slider.onValueChanged.AddListener(delegate { OnValueChanged();  });
        }

        private void InitSlider()
        {
            slider.minValue = 0f;
            slider.maxValue = 100f;

            name = gameObject.name;
            if (name == "MusicSlider")
            {
                slider.value = settings.settingsOptions._musicLevel;
            }

            if (name == "SoundSlider")
            {
                slider.value = settings.settingsOptions._soundLevel;
            }
        }

        private void OnValueChanged()
        {
            var settingsOptions = settings.settingsOptions;

            if (name == "MusicSlider")
            {
                settingsOptions._musicLevel = slider.value;    
            }

            if (name == "SoundSlider")
            {
                settingsOptions._soundLevel = slider.value;
            }
        }
    }
}