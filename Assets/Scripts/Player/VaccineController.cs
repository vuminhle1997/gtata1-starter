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
            var currentGameState = playerController.GetCurrentGameState();
            if (currentGameState != GameState.Play) return;
            _lookDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

            _lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
                
                // Debug.Log(_lookAngle);
            transform.rotation = Quaternion.Euler(0f, 0f, _lookAngle - 90f);
        }

        public void FireVaccine()
        {
            GameObject vaccine = Instantiate(vaccineBullet, vaccineTip.position, vaccineTip.rotation);
            vaccine.GetComponent<Rigidbody2D>().velocity = vaccineTip.up * 100f;
        }
    }
}
