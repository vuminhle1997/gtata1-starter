using System;
using UnityEngine;

namespace Player
{
    public class VaccineController : MonoBehaviour
    {
        [SerializeField] private Transform vaccineTip;

        [SerializeField] private GameObject vaccineBullet;

        private Vector2 lookDirection;
        private float lookAngle;


        private void Update()
        {
            var dir = Input.mousePosition;
            lookDirection = Input.mousePosition - Camera.main.ScreenToWorldPoint(transform.position);
            
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            // Debug.Log(lookDirection);
            // Debug.Log(lookAngle);
            transform.rotation = Quaternion.Euler(0f, 0f, lookAngle - 90f);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                FireVaccine();
            }
        }

        private void FireVaccine()
        {
            GameObject vaccine = Instantiate(vaccineBullet, vaccineTip.position, vaccineTip.rotation);
            vaccine.GetComponent<Rigidbody2D>().velocity = vaccineTip.up * 100f;
        }
    }
}
