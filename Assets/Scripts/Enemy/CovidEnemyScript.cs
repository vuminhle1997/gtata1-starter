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
    
    /// <summary>
    /// The enemy script
    /// </summary>
    public class CovidEnemyScript : MonoBehaviour
    {
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
        
        #region Setters

        /// <summary>
        /// Sets the enemy stats based on the difficulty
        /// </summary>
        /// <param name="difficulty"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void InitCovidStats(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    health = 1;
                    enemyDifficulty = EnemyDifficulty.Easy;
                    break;
                case Difficulty.Medium:
                    health = 2;
                    enemyDifficulty = EnemyDifficulty.Medium;
                    break;
                case Difficulty.Hard:
                    health = 3;
                    enemyDifficulty = EnemyDifficulty.Hard;
                    break;
                default:
                    throw new NotImplementedException("Not implemented");
            }
        }
        
        /// <summary>
        /// Attach the points tracker to this enemy.
        /// </summary>
        /// <param name="pointsTracker"></param>
        public void SetPointsTracker(PointsTracker pointsTracker)
        {
            _pointsTracker = pointsTracker;
        }

        #endregion

        /// <summary>
        /// If the enemy gets hit by a vaccine, decremented the HP and eventually kill this
        /// enemy. Also, destroy the vaccine projectile from the game.
        /// Increment the player's score
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter2D(Collision2D other)
        {
            var vaccineCollider = other.contacts[0].collider;
            if (vaccineCollider != null && vaccineCollider.name.ToLower().Contains("vaccine"))
            {
                var vaccineGameObject = vaccineCollider.gameObject;
                Destroy(vaccineGameObject);
                health -= 1;
                
                _pointsTracker.playerScore.CurrentScore += 100;
            }
        }

        /// <summary>
        /// Increments the player's score based on the difficulty.
        /// Kills enemy and removes it from the game.
        /// </summary>
        private void AddPointsBasedOnDifficulty()
        {
            switch (enemyDifficulty)
            {
                case EnemyDifficulty.Easy:
                    _pointsTracker.playerScore.CurrentScore += 100;
                    break;
                case EnemyDifficulty.Medium:
                    _pointsTracker.playerScore.CurrentScore += 200;
                    break;
                case EnemyDifficulty.Hard:
                    _pointsTracker.playerScore.CurrentScore += 300;
                    break;
            }
            
            Destroy(gameObject);
        }
    }
}
