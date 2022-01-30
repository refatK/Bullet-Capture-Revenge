
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemySelector 
{
    public List<GameObject> mAllEnemies;

    public enum EnemyVariant 
    {
        Basic = 0,
        Shotgun,
        Tracker
    }

    public List<GameObject> GetAllEnemyList() 
    {
        return mAllEnemies;
    }

    public GameObject Select(EnemyVariant variant)
    {
        return mAllEnemies[((int)variant)];
    }
}
