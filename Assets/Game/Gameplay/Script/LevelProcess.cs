using System;
using System.Collections;
using System.Collections.Generic;
using Funzilla;
using UnityEngine;
using UnityEngine.UI;

public class LevelProcess : MonoBehaviour
{
    [SerializeField] private List<Image> img;
    private int enemy;
    [SerializeField] private List<float> boss;
    [SerializeField] private List<Animator> anim;
    internal static LevelProcess Instance;
    private int currentEnemy;
    private float currentBoss;
    private int level;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // foreach (var m in milestones)
        // {
        //     m.SetActive(false);
        // }
        currentEnemy = SetEnemyAmount();
        enemy = SetEnemyAmount();
        level = Profile.Level-1;
        currentBoss = boss[Profile.Level-1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private int SetEnemyAmount()
    {
        enemy = 0;
        switch (Profile.Level)
        {
            case 1: enemy = 11;
                break;
            case 2: enemy = 32;
                break;
            case 3: enemy = 46;
                break;
                
        }
        return enemy;
    }
    
    public void EnemyProcess()
    {
        currentEnemy -= 1;
        img[0].fillAmount = (float)currentEnemy / enemy;
        // if (currentEnemy == 4 ||
        //     currentEnemy == 10 ||
        //     currentEnemy == 12 ||
        //     currentEnemy == 32 ||
        //     currentEnemy == 16 ||
        //     currentEnemy == 46)
        // {
        //     Milestone();
        // }
        Milestone();
    }

    public void BossProcess(float damage)
    {
        currentBoss -= damage; 
        img[1].fillAmount = currentBoss / boss[level];
        if(currentBoss <= 0) anim[2].SetTrigger("anim");

    }

    private void Milestone()
    {
        switch (level)
        {
            case 0:
            {
                if (currentEnemy == 6)
                {
                    anim[0].SetTrigger("anim");
                }

                if (currentEnemy == 0)
                {
                    anim[1].SetTrigger("anim");
                }

                break;
            }
            case 1:
            {
                if (currentEnemy == 20)
                {
                    anim[0].SetTrigger("anim");
                }

                if (currentEnemy == 0)
                {
                    anim[1].SetTrigger("anim");
                }

                break;
            }
            case 2:
            {
                if (currentEnemy == 30)
                {
                    anim[0].SetTrigger("anim");
                }

                if (currentEnemy == 0)
                {
                    anim[1].SetTrigger("anim");
                }

                break;
            }
        }
    }
}
