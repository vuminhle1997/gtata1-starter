using System;
using UnityEngine;

namespace Enemy
{
    public class CovidEnemyMovementAI : MonoBehaviour
    {
        private CovidEnemyScript _covidEnemyScript;
        private float dirX;
        private Vector2 originPos;
        public bool moveForward;

        private void Awake()
        {
            _covidEnemyScript = gameObject.GetComponent<CovidEnemyScript>();
            dirX = transform.position.x;
            originPos = transform.position;
            moveForward = true;
        }

        // Update is called once per frame
        void Update()
        {
            MoveCovidEnemy();
        }

        private void FixedUpdate()
        {
            MovementCycle();
        }

        private void MoveCovidEnemy()
        {
            if (moveForward)
            {
                dirX = 0.125f * _covidEnemyScript.moveSpeed * Time.deltaTime;
            }
            else
            {
                dirX = -0.125f * _covidEnemyScript.moveSpeed * Time.deltaTime;
            }

            Vector2 newPos = new Vector2(transform.position.x + dirX, transform.position.y);
            transform.position = newPos;
        }

        private void MovementCycle()
        {
            Vector2 newPos = transform.position;
            var x = Math.Pow((originPos.x - newPos.x), 2);
            var y = Math.Pow((originPos.y - newPos.y), 2);

            var d = Math.Sqrt(x + y);
            if (d >= 150f)
            {
                moveForward = false;
                return;
            }

            if (d <= 90f)
            {
                moveForward = true;
            }
        }
    }
}
