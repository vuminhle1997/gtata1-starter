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
        public float _health;
        public int _bullets;

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
        public PlayerStats _playerStats;
        
        [SerializeField] private PlayerStateMachine stateMachine;
        // Start is called before the first frame update
        private float moveSpeed;
        private float dirX, dirY;

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private bool originFlip = true;

        [SerializeField] public float jumpForce;

        public bool isGrounded;
        public Transform groundCheck;
        public LayerMask groundLayer;
        private void Awake()
        {
            _playerStats = new PlayerStats(100f, 10);
            moveSpeed = 100f;
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            dirX = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        
            // triggers walking or idle transition
            if (dirX == 0)
            {
                stateMachine.Trigger(PlayerTransition.IsIdle, null);
            }
            else
            {
                stateMachine.Trigger(PlayerTransition.IsWalking, null);
            }

            // triggers jumping transition
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.Trigger(PlayerTransition.IsJumping, null);
            }
        }

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
            // transition to falling down state
            if (!isGrounded && stateMachine.GetCurrentState() == PlayerState.Jump)
            {
                stateMachine.Trigger(PlayerTransition.IsFallingDown, null);
            }
            
            CheckHealth();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            foreach (var contactPoint2D in other.contacts)
            {
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
            if (_playerStats._health <= 0f)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// performs jump on player
        /// </summary>
        public void Jump()
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        public float GetDirX()
        {
            return dirX;
        }

        public void TweakMovementSpeed(float val)
        {
            moveSpeed = val;
        }

        public PlayerStats GetPlayerStats()
        {
            return _playerStats;
        }
    }
}
