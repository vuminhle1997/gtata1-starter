using System;
using Enemy;
using StateMachines;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Information about the player.
    /// Contains health and current bullets.
    /// </summary>
    public class PlayerStats
    {
        private float _health;
        private int _bullets;

        public PlayerStats(float health, int bullets)
        {
            _health = health;
            _bullets = bullets;
        }

        public float GetHealth()
        {
            return _health;
        }

        public void ChangeHealth(float health)
        {
            _health += health;
        }

        public int GetBullets()
        {
            return _bullets;
        }

        public void ChangeBullets(int bullets)
        {
            _bullets += bullets;
        }
    }
    public class PlayerController : MonoBehaviour
    {
        private PlayerStats _playerStats;
        
        [SerializeField] private PlayerStateMachine stateMachine;
        [SerializeField] private GameStateMachine _gameStateMachine;

        private GameState _currentGameState;
        // Start is called before the first frame update
        private float _moveSpeed;
        private float _dirX, _dirY;

        private Rigidbody2D _rb;

        [SerializeField] public float jumpForce;

        public bool isGrounded;
        public Transform groundCheck;
        public LayerMask groundLayer;
        private void Awake()
        {
            _playerStats = new PlayerStats(100f, 10);
            _moveSpeed = 100f;
            _rb = GetComponent<Rigidbody2D>();
            _currentGameState = _gameStateMachine.GetCurrentGameState();
        }

        // Update is called once per frame
        void Update()
        {
            _dirX = Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.deltaTime;
        
            // triggers walking or idle transition
            if (_dirX == 0)
            {
                stateMachine.Trigger(PlayerTransition.IsIdle, null);
            }
            else
            {
                stateMachine.Trigger(PlayerTransition.IsWalking, null);
            }

            // triggers jumping transition
            if (Input.GetKeyDown(KeyCode.Space) && _currentGameState == GameState.Play)
            {
                stateMachine.Trigger(PlayerTransition.IsJumping, null);
            }
        }

        private void FixedUpdate()
        {
            _currentGameState = _gameStateMachine.GetCurrentGameState();
            // isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
            switch (_currentGameState)
            {
                case GameState.Play:
                    _rb.WakeUp();
                    break;
                case GameState.Menu:
                    _rb.Sleep();
                    break;
            }
            
            CheckHealth();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            foreach (var contactPoint2D in other.contacts)
            {
                if (contactPoint2D.collider.name.ToLower().Contains("layer"))
                {
                    stateMachine.Trigger(PlayerTransition.IsFallingDown);
                    continue;
                }
                if (contactPoint2D.collider.name.ToLower().Contains("enemy"))
                {
                    var enemyGameObject = contactPoint2D.collider.gameObject;

                    var enemyScript = enemyGameObject.GetComponent<CovidEnemyScript>();
                    var type = enemyScript.enemyDifficulty;

                    switch (type)
                    {
                        case EnemyDifficulty.Easy:
                            _playerStats.ChangeHealth(-20f);
                            break;
                        case EnemyDifficulty.Medium:
                            _playerStats.ChangeHealth(-50f);
                            break;
                        case EnemyDifficulty.Hard:
                            _playerStats.ChangeHealth(-100f);
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    Destroy(enemyGameObject);
                }
            }
        }

        private void CheckHealth()
        {
            if (_playerStats.GetHealth() <= 0f)
            {
                _gameStateMachine.Trigger(GameTransition.ShowGameOver);
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// performs jump on player
        /// </summary>
        public void Jump()
        {
            _rb.velocity = Vector2.up * jumpForce;
        }

        public float GetDirX()
        {
            return _dirX;
        }

        public void TweakMovementSpeed(float val)
        {
            _moveSpeed = val;
        }

        public PlayerStats GetPlayerStats()
        {
            return _playerStats;
        }

        public GameState GetCurrentGameStateFromPlayerParent()
        {
            return _currentGameState;
        }
    }
}
