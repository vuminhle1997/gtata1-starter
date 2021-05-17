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
        
        private void Update()
        {
            var health = (int) playerController.GetPlayerStats().GetHealth();

            if (health <= 0f)
            {
                textUI.text = "Health: " + 0;
                return;
            }
            textUI.text = "Health: " + $"{health}";
        }
    }
}