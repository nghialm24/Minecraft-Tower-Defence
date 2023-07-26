using System.Collections;
using System.Collections.Generic;
using Funzilla;
using UnityEngine;

[System.Serializable]
public class DataConfig
{
    [Header("=======World========")] 
    public List<WorldData> worldData;

    [Header("=======Tower========")] 
    public List<TowerData> towerData;
    public Bullet bullet;
    public List<IngredientData> ingredientData;

}

[System.Serializable]
public class IngredientData
{
    public Ingredient.TypeItem type;
    public Sprite avatar;
}

[System.Serializable]
public class WorldData
{
    public int id;
    public int delayPerLevel;
    public List<LevelData> levelData;
}

[System.Serializable]
public class LevelData
{
    public int levelID;
    public float delayPerWave = 15.0f;
}

[System.Serializable]
public class TowerData
{
    public int level;
    public int damage;
    public float atkSpeed;
    public float range;
}