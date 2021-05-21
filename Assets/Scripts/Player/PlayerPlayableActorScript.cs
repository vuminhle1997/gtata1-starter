using System;
using Actor;
using Enemy;
using StateMachines;
using UnityEngine;

namespace Player
{
    public class PlayerPlayableActorScript: PlayableActor
    {
        private Rigidbody2D rb;
        private float jumpForce = 125f;
        [SerializeField] private VaccineController vaccineController;
        [SerializeField] private PlayerStateMachine stateMachine;

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
                            Health -= 20f;
                            break;
                        case EnemyDifficulty.Medium:
                            Health -= 50f;
                            break;
                        case EnemyDifficulty.Hard:
                            Health -= 100f;
                            break;
                    }
                    Destroy(enemyGameObject);
                }
            }
        }

        private void CheckHealth()
        {
            if (Health <= 0f)
            {
                Alive = false;
            }
        }
    }
}