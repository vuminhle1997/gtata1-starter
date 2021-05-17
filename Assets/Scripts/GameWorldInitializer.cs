using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Persistence;
using UnityEngine;
using Random = System.Random;

public class GameWorldInitializer : MonoBehaviour
{
    public SettingsOptions settings;
    [SerializeField] private GameObject[] spawnAreas;
    [SerializeField] private GameObject[] enemiesObject;
    
    public List<GameObject> placedAreas;
    public List<GameObject> placedEnemies;
    private void Awake()
    {
        var _settings = Settings.LoadSettings(Settings.PATH);
        settings = _settings;
        
        SpawnEnemiesBasedOnDifficulty(_settings._difficulty);
    }

    private void SpawnEnemiesBasedOnDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                var oneThird = spawnAreas.Length / 3;
                SpawnEnemies(oneThird, Difficulty.Easy);
                break;
            case Difficulty.Medium:
                var twoThird = (spawnAreas.Length / 3) * 2;
                SpawnEnemies(twoThird, Difficulty.Medium);
                break;
            case Difficulty.Hard:
                var fullLength = spawnAreas.Length;
                SpawnEnemies(fullLength, Difficulty.Hard, true);
                break;
        }
    }

    private void SpawnEnemies(int count, Difficulty difficulty, bool isHardestDifficulty = false)
    {
        Random random = new Random();
        if (isHardestDifficulty)
        {
            foreach (var spawnLocation in spawnAreas)
            {
                placedAreas.Add(spawnLocation);
                Debug.Log(spawnLocation.transform.position);
                
                GameObject enemyPrefabPreset = enemiesObject[random.Next(enemiesObject.Length)];
                GameObject theSpawnedEnemy = Instantiate(enemyPrefabPreset, spawnLocation.transform);
            
                theSpawnedEnemy.GetComponent<CovidEnemyScript>().InitCovidStats(difficulty);
                placedEnemies.Add(theSpawnedEnemy);
            }
            return;
        }
        
        for (var i = 0; i < count; i++)
        {
            int nextInt = random.Next(spawnAreas.Length);
            GameObject spawnLocation = spawnAreas[nextInt];

            while (placedAreas.Contains(spawnLocation))
            {
                nextInt = random.Next(spawnAreas.Length);
                spawnLocation = spawnAreas[nextInt];
            }
            placedAreas.Add(spawnLocation);
            GameObject enemyPrefabPreset = enemiesObject[random.Next(enemiesObject.Length)];
            GameObject theSpawnedEnemy = Instantiate(enemyPrefabPreset, spawnLocation.transform);
            
            theSpawnedEnemy.GetComponent<CovidEnemyScript>().InitCovidStats(difficulty);
            placedEnemies.Add(theSpawnedEnemy);
            
            Debug.Log(spawnLocation.transform.position);
        }
    }
}
