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

        /// <summary>
        /// Script for point tracker in game over screen and in-game UI.
        /// If it is the game over screen, shot the text inside the if-scope.
        /// </summary>
        private void Awake()
        {
            _pointsUI = gameObject.GetComponent<TextMeshProUGUI>();
            
            var gameObjectName = gameObject.name;
            if (gameObjectName == "Points")
            {
                _pointsUI.text = $"{pointsTracker.playerScore.CurrentScore}" + " Pts";
            }
        }

        /// <summary>
        /// Re-assignment for the the points tracker inside the in-game UI.
        /// If it is the game-over screen, do nothing more!
        /// </summary>
        private void Update()
        {
            var gameObjectName = gameObject.name;
            if (gameObjectName == "Points")
            {
                return;
            }
            _pointsUI.text = $"{pointsTracker.playerScore.CurrentScore}";
        }
    }
}