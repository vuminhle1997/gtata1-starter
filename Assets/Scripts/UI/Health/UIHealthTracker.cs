using System;
using Player;
using TMPro;
using UnityEngine;

namespace UI.Health
{
    public class UIHealthTracker : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        private TextMeshProUGUI textUI;
        
        private void Awake()
        {
            textUI = gameObject.GetComponent<TextMeshProUGUI>();
        }
        
        /// <summary>
        /// Attaches the player's HP to the UI text.
        /// </summary>
        private void Update()
        {
            var health = (int) playerController.Actor.Health;

            if (health <= 0f)
            {
                textUI.text = "Health: " + 0;
                return;
            }
            textUI.text = "Health: " + $"{health}";
        }
    }
}