using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Funzilla;
using UnityEngine;
using UnityEngine.UI;

public class AnimUI : MonoBehaviour
{
    private DataConfig _dataConfig => Gameplay.Instance.dataConfigSO.DataConfig;
    public static AnimUI Instance;
    [SerializeField] private List<Animator> anim;
    [SerializeField] private Image enemyBar;
    [SerializeField] private Image bossBar;
    private int currentEnemy;
    private int enemyHp;
    private float bossHp;
    private float currentBoss;
    private int level;

    private void Awake()
    {
        Instance = this;
    }

    private void CheckRound()
    {
        for (int i = 0; i < _dataConfig.; i++)
        {
            
        }
    }
    
    public void SetData(int e, int b)
    {
        enemyHp = e;
        currentEnemy = e;
        level = Profile.Level-1;
        currentBoss = b;
        bossHp = b;
    }

    public void EnemyProcess()
    {
        currentEnemy -= 1;
        enemyBar.fillAmount = (float)currentEnemy / enemyHp;
        Milestone();
    }
    
    public void BossProcess(float damage)
    {
        currentBoss -= damage; 
        bossBar.fillAmount = currentBoss / bossHp;
        if (currentBoss <= 0)
        {
            if(level == 1) anim[4].SetTrigger("anim");
            if(level == 2) anim[6].SetTrigger("anim");
        }
    }
    
    private void Milestone()
    {
        Debug.Log(level);

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
                if (currentEnemy == 15)
                { 
                    anim[0].SetTrigger("anim");
                }

                if (currentEnemy == 11)
                {
                    anim[1].SetTrigger("anim");
                }
                
                if (currentEnemy == 8)
                {
                    anim[2].SetTrigger("anim");
                }
                
                if (currentEnemy == 0)
                {
                    anim[3].SetTrigger("anim");
                }
                break;
            }
            case 2:
            {
                if (currentEnemy == 30)
                { 
                    anim[0].SetTrigger("anim");
                }

                if (currentEnemy == 28)
                {
                    anim[1].SetTrigger("anim");
                }
                
                if (currentEnemy == 25)
                {
                    anim[2].SetTrigger("anim");
                }
                
                if (currentEnemy == 24)
                {
                    anim[3].SetTrigger("anim");
                }
                
                if (currentEnemy == 14)
                {
                    anim[4].SetTrigger("anim");
                }
                
                if (currentEnemy == 0)
                {
                    anim[5].SetTrigger("anim");
                }

                break;
            }
        }
    }
}
