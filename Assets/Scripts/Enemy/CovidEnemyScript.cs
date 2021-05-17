using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Enemy
{
    public enum EnemyDifficulty
    {
        Easy,
        Medium, 
        Hard
    }
    
    public class CovidEnemyScript : MonoBehaviour
    {
        public float moveSpeed;
        public float health;
        public EnemyDifficulty enemyDifficulty;
        private PointsTracker _pointsTracker;
        private void FixedUpdate()
        {
            if (health <= 0)
            {
                AddPointsBasedOnDifficulty();
                
            }
        }

        private void AddPointsBasedOnDifficulty()
        {
            switch (enemyDifficulty)
            {
                case EnemyDifficulty.Easy:
                    _pointsTracker.playerScore.AddCurrentScore(100);
                    break;
                case EnemyDifficulty.Medium:
                    _pointsTracker.playerScore.AddCurrentScore(200);
                    break;
                case EnemyDifficulty.Hard:
                    _pointsTracker.playerScore.AddCurrentScore(300);
                    break;
            }
            
            Destroy(gameObject);
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
                
                _pointsTracker.playerScore.AddCurrentScore(100);
            }
        }

        public void SetPointsTracker(PointsTracker pointsTracker)
        {
            _pointsTracker = pointsTracker;
        }
    }
}
