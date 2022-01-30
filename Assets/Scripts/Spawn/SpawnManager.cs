using System;
using System.Collections.Generic;
using UnityEngine;
using static SpawnSet.Spawn;

[Serializable]
public class SpawnManager
{
    public EnemySelector mEnemySelector;
    public List<SpawnSet> mSpawnSets;

    private int currentSet = 0;
    private bool readyToSpawn = true;

    public void SpawnCurrentSet() {
        mSpawnSets[currentSet].SpawnCurrentSet(mEnemySelector);
        currentSet += 1;
        readyToSpawn = false;
    }

    public void StartNextSpawnSet() {
        if (IsComplete())
        {
            readyToSpawn = false;
        }

        readyToSpawn = true;
    }

    public bool IsReadyToSpawn()
    {
        return readyToSpawn;
    }

    public bool IsComplete()
    {
        return currentSet >= mSpawnSets.Count;
    } 
}