using Persistence;
using UnityEngine;

namespace UI.Toggle
{
    public class UIToggleController : MonoBehaviour
    {
        [SerializeField] private Settings settings;
        private UnityEngine.UI.Toggle toggle;
        private string name;
        
        void Start()
        {
            toggle = GetComponent<UnityEngine.UI.Toggle>();
            name = gameObject.name;

            if (name == "MusicToggle")
            {
                toggle.isOn = settings.settingsOptions._enableMusic;
            }

            if (name == "SoundToggle")
            {
                toggle.isOn = settings.settingsOptions._enableSound;
            }
            
            toggle.onValueChanged.AddListener(delegate { OnValueChanged(); });
        }

        private void OnValueChanged()
        {
            var settingsOptions = settings.settingsOptions;

            if (name == "MusicToggle")
            {
                settingsOptions._enableMusic = toggle.isOn;
            }

            if (name == "SoundToggle")
            {
                settingsOptions._enableSound = toggle.isOn;
            }
        }
    }
}
