using System;
using System.Collections.Generic;
using UnityEngine;
using static EnemySelector;
using static SpawnSet.Spawn;

[Serializable]
public class SpawnSet
{
    [Serializable]
    public class Spawn {
        public EnemyVariant enemyVariant;
        public Vector2 pos;
    }

    public List<Spawn> spawns;
    
    public void SpawnCurrentSet(EnemySelector enemySelector) {
        foreach (Spawn spawn in spawns)
        {
            DoSpawn(spawn, enemySelector);
        }
    }

    private void DoSpawn(Spawn spawn, EnemySelector enemySelector)
    {
        GameObject enemy = enemySelector.Select(spawn.enemyVariant);
        GameObject.Instantiate(enemy, new Vector3(spawn.pos.x, spawn.pos.y, 0), enemy.transform.rotation);
    }
}