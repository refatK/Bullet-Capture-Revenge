using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SpawnSet.Spawn;

public class EnemySpawner : MonoBehaviour
{
    public SpawnManager spawnManager;
    
    void Update() {
        if (spawnManager.IsReadyToSpawn())
        {
            spawnManager.SpawnCurrentSet();
        }

        if (!EnemiesExist())
        {
            spawnManager.StartNextSpawnSet();
        }

        if (!EnemiesExist() && spawnManager.IsComplete())
        {
            Destroy(this.gameObject);
        }
    }
    public void SpawnEnemy(GameObject enemyType, Vector2 pos) {
        Instantiate(enemyType, new Vector3(pos.x, pos.y, 0), enemyType.transform.rotation);
    }

    private bool EnemiesExist()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length > 0; 
    }
}
