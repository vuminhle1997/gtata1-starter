using Actor;
using Enemy;
using StateMachines;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Player
{
    public class PlayerPlayableActorScript: PlayableActor
    {
        private Rigidbody2D rb;
        private float jumpForce = 125f;
        [SerializeField] private VaccineController vaccineController;
        [FormerlySerializedAs("stateMachine")] [SerializeField] private PlayerStateMachine playerStateMachine;
        [SerializeField] private PlayerController playerController;

        /// <summary>
        /// Initialize player stats.
        /// </summary>
        private void Awake()
        {
            Health = 100f;
            Bullets = 10;
            Alive = true;
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CheckHealth();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            foreach (var contactPoint2D in other.contacts)
            {
                // if player touches ground or an obstacle while jumping, switch to idle sprite 
                if (contactPoint2D.collider.gameObject.layer == Layers.Ground || contactPoint2D.collider.gameObject.layer 
                    == Layers.Obstacle)
                {
                    playerStateMachine.Trigger(PlayerTransition.IsFallingDown);
                    continue;
                }
                // if the contact point is an enemy, take damage and destroy enemy
                if (contactPoint2D.collider.gameObject.layer == Layers.Enemy)
                {
                    var enemyGameObject = contactPoint2D.collider.gameObject;

                    var enemyScript = enemyGameObject.GetComponent<CovidEnemyScript>();
                    var type = enemyScript.enemyDifficulty;

                    switch (type)
                    {
                        case EnemyDifficulty.Easy:
                            TakeDamage(-20f);
                            break;
                        case EnemyDifficulty.Medium:
                            TakeDamage(-40f);
                            break;
                        case EnemyDifficulty.Hard:
                            TakeDamage(-100f);
                            break;
                    }
                    Destroy(enemyGameObject);
                }
            }
        }

        /// <summary>
        /// Check player's health!
        /// If the HP is below 0, set Alive to false (player is dead)
        /// </summary>
        private void CheckHealth()
        {
            if (Health <= 0f)
            {
                Alive = false;
            }
        }

        #region ActorCommands

        public override void Jump()
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        public override void FireBullet()
        {
            if (Bullets > 0)
            {
                vaccineController.FireVaccine();
                Bullets--;
            }
        }

        public override void Run()
        {
            playerController.IsRunning = true;
        }

        #endregion
    }
}