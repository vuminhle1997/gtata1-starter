using System;
using System.Collections.Generic;
using Persistence;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Misc
{
    /// <summary>
    /// This class is responsible for the difficulty dropdown
    /// </summary>
    public class DropDownController : MonoBehaviour
    {
        // the setting's values
        [SerializeField] private Settings _settings;
        /// <summary>
        /// source: https://www.youtube.com/watch?v=URS9A4V_yLc
        /// </summary>
        private void Start()
        {
            var dropDown = transform.GetComponent<Dropdown>();

            dropDown.options.Clear();

            List<string> options = new List<string>();
            options.Add("Easy");
            options.Add("Medium");
            options.Add("Hard");

            foreach (var option in options)
            {
                dropDown.options.Add(new Dropdown.OptionData() { text = option });
            }

            InitDropDownValue(dropDown);
        
            dropDown.onValueChanged.AddListener(delegate
                {
                    DropDownItemSelected(dropDown);
                }
            );
        }

        /// <summary>
        /// Changes the difficulty
        /// </summary>
        /// <param name="dropDown"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void DropDownItemSelected(Dropdown dropDown)
        {
            var i = dropDown.value;
        
            Debug.Log("Dropdown Selected: " + $"{dropDown.options[i].text}");
            var settingsOptions = _settings.settingsOptions;
            switch (i)
            {
                case 0:
                    settingsOptions._difficulty = Difficulty.Easy;
                    break;
                case 1:
                    settingsOptions._difficulty = Difficulty.Medium;
                    break;
                case 2:
                    settingsOptions._difficulty = Difficulty.Hard;
                    break;
                default:
                    throw new NotImplementedException("Not implemented");
            }
        }

        /// <summary>
        /// Loads the selected difficulty of the player. If there are no settings locally store, choose EASY instead.
        /// </summary>
        /// <param name="dropDown"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void InitDropDownValue(Dropdown dropDown)
        {
            if (_settings.settingsOptions != null)
            {
                var settingsOptions = _settings.settingsOptions;
                switch (settingsOptions._difficulty)
                {
                    case Difficulty.Easy:
                        dropDown.value = 0;
                        dropDown.captionText.text = dropDown.options[0].text;
                        break;
                    case Difficulty.Medium:
                        dropDown.value = 1;
                        dropDown.captionText.text = dropDown.options[1].text;
                        break;
                    case Difficulty.Hard:
                        dropDown.value = 2;
                        dropDown.captionText.text = dropDown.options[2].text;
                        break;
                    default:
                        throw new NotImplementedException();
                }

                return;
            }
            dropDown.value = 0;
            dropDown.captionText.text = dropDown.options[0].text;
        }
    }
}
