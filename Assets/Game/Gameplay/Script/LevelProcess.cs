using System;
using System.Collections;
using System.Collections.Generic;
using Funzilla;
using UnityEngine;
using UnityEngine.UI;

public class LevelProcess : MonoBehaviour
{
    public static LevelProcess Instance;
    private int enemy;
    [SerializeField] private List<float> boss;
    [SerializeField] private List<AnimUI> listAnimUi;
    private int currentBoss;
    private int level;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        enemy = SetEnemyAmount();
        level = Profile.Level-1;
        currentBoss = (int)boss[Profile.Level-1];

        listAnimUi[level].SetData(enemy, currentBoss);
        for (int i = 0; i < listAnimUi.Count; i++)
        {
            listAnimUi[i].gameObject.SetActive(level == i);
        }
    }

    private int SetEnemyAmount()
    {
        enemy = 0;
        switch (Profile.Level)
        {
            case 1: enemy = 10;
                break;
            case 2: enemy = 19;
                break;
            case 3: enemy = 32;
                break;
                
        }
        return enemy;
    }
}
