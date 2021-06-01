using System;
using Game;
using TMPro;
using UnityEngine;

namespace UI.Misc
{
    public class UIInputFieldController: MonoBehaviour
    {
        private TMP_InputField inputField;
        private String name;

        private void Awake()
        {
            inputField = gameObject.GetComponent<TMP_InputField>();
        }

        private void Update()
        {
            name = inputField.text;
        }

        public string GetName()
        {
            return name;
        }
    }
}