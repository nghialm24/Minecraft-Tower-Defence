using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Funzilla;
using UnityEngine;
using UnityEngine.UI;

public class AnimUI : MonoBehaviour
{
    public static AnimUI Instance;
    [SerializeField] private DataEnemy dataEnemy;
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

    void Start()
    {
        anim = GetComponentsInChildren<Animator>().ToList();
        bossHp = dataEnemy.listEnemyConfig[Profile.Level - 1].hp;
    }

    public void EnemyProcess()
    {
        currentEnemy -= 1;
        enemyBar.fillAmount = (float)currentEnemy / enemyHp;
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
        bossBar.fillAmount = currentBoss / bossHp;
        //if(currentBoss <= 0) anim[2].SetTrigger("anim");

    }
    
    private void Milestone()
    {
        switch (level)
        {
            case 0:
            {
                if (currentEnemy == 6)
                {
                    //anim[0].SetTrigger("anim");
                }

                if (currentEnemy == 0)
                {
                    //anim[1].SetTrigger("anim");
                }

                break;
            }
            case 1:
            {
                if (currentEnemy == 20)
                {
                    //anim[0].SetTrigger("anim");
                }

                if (currentEnemy == 0)
                {
                    //anim[1].SetTrigger("anim");
                }

                break;
            }
            case 2:
            {
                if (currentEnemy == 30)
                {
                    //anim[0].SetTrigger("anim");
                }

                if (currentEnemy == 0)
                {
                    //anim[1].SetTrigger("anim");
                }

                break;
            }
        }
    }
}
