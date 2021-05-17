using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class CovidStats
    {
        public float moveSpeed;
        public float health;

        public CovidStats(float moveSpeed, float health)
        {
            this.health = moveSpeed;
            this.health = health;
        }
    }
    public class CovidEnemyScript : MonoBehaviour
    {
        public CovidStats stats;
        private void Awake()
        {
            // InitCovidStats(loader.settings._difficulty);
        }

        public void InitCovidStats(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    stats = new CovidStats(100f, 1);
                    break;
                case Difficulty.Medium:
                    stats = new CovidStats(100f, 2);
                    break;
                case Difficulty.Hard:
                    stats = new CovidStats(100f, 3);
                    break;
                default:
                    throw new NotImplementedException("Not implemented");
            }
            
            Debug.Log(stats.health);
        }

        public void SetHealth(float health)
        {
            stats.health = health;
        }

        public void SetMoveSpeed(float moveSpeed)
        {
            stats.moveSpeed = moveSpeed;
        }

        public float GetHealth()
        {
            return stats.health;
        }

        public float GetMoveSpeed()
        {
            return stats.moveSpeed;
        }
    }
}
