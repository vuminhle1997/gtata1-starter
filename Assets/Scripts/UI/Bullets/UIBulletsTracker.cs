using System;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Bullets
{
    public class UIBulletsTracker : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        private TextMeshProUGUI _bulletsUI;

        private void Awake()
        {
            _bulletsUI = gameObject.GetComponent<TextMeshProUGUI>();
        }

        private void FixedUpdate()
        {
            _bulletsUI.text = "Bullets: " + $"{playerController.GetPlayerStats().GetBullets()}";
        }
    }
}
