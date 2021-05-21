using System;
using StateMachines;
using UnityEngine;

namespace Enemy
{
    public class CovidEnemyMovementAI : MonoBehaviour
    {
        private CovidEnemyScript _covidEnemyScript;
        private float _dirX;
        private Vector2 _originPos;
        private bool moveForward;
        private GameState _currentGameState;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _covidEnemyScript = gameObject.GetComponent<CovidEnemyScript>();
            _dirX = transform.position.x;
            _originPos = transform.position;
            moveForward = true;
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentGameState == GameState.Play)
            {
                if (!_rb.IsAwake())
                {
                    _rb.WakeUp();    
                }
                MoveCovidEnemy();
            }
            else if (_currentGameState == GameState.Menu)
            {
                _rb.Sleep();
            }
        }

        private void FixedUpdate()
        {
            MovementCycle();
        }

        private void MoveCovidEnemy()
        {
            if (moveForward)
            {
                _dirX = 0.125f * _covidEnemyScript.moveSpeed * Time.deltaTime;
            }
            else
            {
                _dirX = -0.125f * _covidEnemyScript.moveSpeed * Time.deltaTime;
            }

            Vector2 newPos = new Vector2(transform.position.x + _dirX, transform.position.y);
            transform.position = newPos;
        }

        private void MovementCycle()
        {
            Vector2 newPos = transform.position;
            var x = Math.Pow((_originPos.x - newPos.x), 2);
            var y = Math.Pow((_originPos.y - newPos.y), 2);

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

        public void SetCurrentGameState(GameState state)
        {
            _currentGameState = state;
        }
    }
}
