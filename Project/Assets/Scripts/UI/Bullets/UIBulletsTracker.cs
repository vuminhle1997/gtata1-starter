using System;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Bullets
{
    /// <summary>
    /// Tracks the player's current bullet
    /// </summary>
    public class UIBulletsTracker : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        private TextMeshProUGUI _bulletsUI;

        private void Awake()
        {
            _bulletsUI = gameObject.GetComponent<TextMeshProUGUI>();
        }

        /// <summary>
        /// Attaches the bullet count to the UI text.
        /// </summary>
        private void Update()
        {
            _bulletsUI.text = "Bullets: " + $"{playerController.Actor.Bullets}";
        }
    }
}
