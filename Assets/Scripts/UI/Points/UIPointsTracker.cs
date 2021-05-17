using System;
using Game;
using TMPro;
using UnityEngine;

namespace UI.Points
{
    public class UIPointsTracker: MonoBehaviour
    {
        [SerializeField] private PointsTracker pointsTracker;
        private TextMeshProUGUI _pointsUI;

        private void Awake()
        {
            _pointsUI = gameObject.GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _pointsUI.text = $"{pointsTracker.playerScore.GetCurrentScore()}";
        }
    }
}