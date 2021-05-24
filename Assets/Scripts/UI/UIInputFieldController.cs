using System;
using Game;
using Score;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIInputFieldController: MonoBehaviour
    {
        [SerializeField] private PointsTracker pointsTracker;
        private TMP_InputField inputField;
        private String name;

        private void Awake()
        {
            inputField = gameObject.GetComponent<TMP_InputField>();
        }

        private void Update()
        {
            name = inputField.text;
            pointsTracker.playerScore.Name = name;
        }

        public string GetName()
        {
            return name;
        }
    }
}