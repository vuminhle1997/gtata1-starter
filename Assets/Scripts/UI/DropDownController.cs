using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DropDownController : MonoBehaviour
    {
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

        private void DropDownItemSelected(Dropdown dropDown)
        {
            var i = dropDown.value;
        
            Debug.Log("Dropdown Selected: " + $"{dropDown.options[i].text}");
        }

        private void InitDropDownValue(Dropdown dropDown)
        {
            dropDown.value = 0;
            dropDown.itemText.text = dropDown.options[0].text;
            dropDown.captionText.text = dropDown.options[0].text;
        }
    }
}
