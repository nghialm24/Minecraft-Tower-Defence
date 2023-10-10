using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataEnemy
{
    public List<EnemyConfig> listEnemyConfig;
    
}
[System.Serializable]
public class EnemyConfig
{
    public TypeEnemy typeEnemy;
    public float hp;
    public float attackSpeed;
    public float moveSpeed;
}

