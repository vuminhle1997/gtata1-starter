using System;
using StateMachines;
using UnityEngine;

namespace Player
{
    public class VaccineController : MonoBehaviour
    {
        [SerializeField] private Transform vaccineTip;

        [SerializeField] private GameObject vaccineBullet;
        [SerializeField] private PlayerController _playerController;

        private Vector2 lookDirection;
        private float lookAngle;

        private void Update()
        {
            var currentGameState = _playerController.GetCurrentGameStateFromPlayerParent();
            if (currentGameState == GameState.Play)
            {
                lookDirection = Input.mousePosition - Camera.main.ScreenToWorldPoint(transform.position);
            
                lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    var playerStats = _playerController.GetPlayerStats();
                    if (playerStats.GetBullets() > 0)
                    {
                        FireVaccine();
                        playerStats.ChangeBullets(-1);
                    }
                    else
                    {
                        Debug.Log("Bullets are empty");
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
