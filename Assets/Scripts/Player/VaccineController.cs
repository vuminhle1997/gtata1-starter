using System;
using StateMachines;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class VaccineController : MonoBehaviour
    {
        [SerializeField] private Transform vaccineTip;

        [SerializeField] private GameObject vaccineBullet;
        [SerializeField] private PlayerController playerController;

        private Vector2 _lookDirection;
        private float _lookAngle;

        private void Update()
        {
            // source: https://answers.unity.com/questions/603757/2d-mouse-aiming.html
            var currentGameState = playerController.GetCurrentGameStateFromPlayerParent();
            if (currentGameState == GameState.Play)
            {
                _lookDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            
                _lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, _lookAngle - 90f);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    var playerStats = playerController.GetPlayerStats();
                    if (playerStats.GetBullets() > 0)
                    {
                        FireVaccine();
                        playerStats.ChangeBullets(-1);
                    }
                }
            }
        }

        private void FireVaccine()
        {
            GameObject vaccine = Instantiate(vaccineBullet, vaccineTip.position, vaccineTip.rotation);
            vaccine.GetComponent<Rigidbody2D>().velocity = vaccineTip.up * 100f;
        }
    }
}
