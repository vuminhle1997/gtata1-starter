using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public enum EnemyDifficulty
    {
        Easy,
        Medium, 
        Hard
    }
    public class CovidStats
    {
        public float moveSpeed;
        public float health;
        public EnemyDifficulty enemyDifficulty;

        public CovidStats(float moveSpeed, float health, EnemyDifficulty enemyDifficulty)
        {
            this.health = moveSpeed;
            this.health = health;
            this.enemyDifficulty = enemyDifficulty;
        }
    }
    public class CovidEnemyScript : MonoBehaviour
    {
        public float moveSpeed;
        public float health;
        public EnemyDifficulty enemyDifficulty;

        private void FixedUpdate()
        {
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void InitCovidStats(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    moveSpeed = 100f;
                    health = 1;
                    enemyDifficulty = EnemyDifficulty.Easy;
                    break;
                case Difficulty.Medium:
                    moveSpeed = 100f;
                    health = 2;
                    enemyDifficulty = EnemyDifficulty.Medium;
                    break;
                case Difficulty.Hard:
                    moveSpeed = 100f;
                    health = 3;
                    enemyDifficulty = EnemyDifficulty.Hard;
                    break;
                default:
                    throw new NotImplementedException("Not implemented");
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var vaccineCollider = other.contacts[0].collider;
            if (vaccineCollider != null && vaccineCollider.name.ToLower().Contains("vaccine"))
            {
                var vaccineGameObject = vaccineCollider.gameObject;
                Destroy(vaccineGameObject);
                health -= 1;
            }
        }
    }
}
