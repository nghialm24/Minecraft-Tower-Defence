using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Funzilla;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private DataEnemy dataEnemy => Gameplay.Instance.dataEnemySO.DataEnemy;
    private List<EnemyConfig> enemyConfigs => dataEnemy.listEnemyConfig;
    [SerializeField] private Row row; 
    [SerializeField] private Transform model;
    private int id;
    [SerializeField] private Image currentHp;
    public float hp;
    private float baseHp;

    [SerializeField] private GameObject canvasHP;
    [SerializeField] private GameObject enemyDeath;
    [SerializeField] private List<SkinnedMeshRenderer> listSkin;
    // public void Init(SplineComputer splineComputer)
    // {
    //     splineFollower = GetComponent<SplineFollower>();
    //     splineFollower.spline = splineComputer;
    //     splineFollower.enabled = true;
    // }
    public TypeEnemy typeEnemy;

    public void Init(TypeEnemy type, float x, float hp)
    {
        typeEnemy = type;
        for (int i = 0; i < model.childCount; i++)
        {
            if(OutputTypeEnemy(typeEnemy) != i)
            {
                model.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                model.GetChild(i).gameObject.SetActive(true);
                canvasHP.transform.position += new Vector3(0,i,0);
            }
        }
        // var c = enemyConfigs.FirstOrDefault(x => x.typeEnemy == typeEnemy);
        // if (c != null)
        // {
        //     baseHp = c.hp;
        // }
        baseHp = hp;
        this.hp = hp;
        transform.localPosition += new Vector3(x,0,0);
    }
    private void Start()
    {
        canvasHP.SetActive(false);
    }
    void Update()
    { 
        if(hp > 0)
            currentHp.fillAmount = hp / baseHp;
        else
        {
            //Destroy(gameObject);
            row.Destroy();
            var eD = Instantiate(enemyDeath, transform.position, Quaternion.identity);
            eD.transform.eulerAngles = transform.eulerAngles;
            eD.transform.GetChild(0).transform.GetChild(OutputTypeEnemy(typeEnemy)).gameObject.SetActive(true);
            //listSkin[OutputTypeEnemy(typeEnemy)].material.color = Color.red);
        }

        if (hp >= baseHp) return;
        canvasHP.SetActive(true);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainHouse"))
        {
            hp = 0;
            row.Destroy();
            if (!Gameplay.Instance.isGameOver)
            {
                Gameplay.Instance.isGameOver = true;
                Gameplay.Instance.Lose();
            }
        }
        
        // if(other.CompareTag("Player"))
        // {
        //     hp = 0;
        //     row.Destroy();
        // }
    }

    private int OutputTypeEnemy(TypeEnemy type)
    {
        var i = 0;
        switch (type)
        {
            case TypeEnemy.ghast:
                i = 0;
                break;
            case TypeEnemy.zombie:
                i = 1;
                break;
            case TypeEnemy.skeleton:
                i = 2;
                break;
            case TypeEnemy.enderman:
                i = 3;
                break;
            case TypeEnemy.thewither:
                i = 4;
                break;
            case TypeEnemy.enderdragon:
                i = 5;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return i;
    }
}
public enum TypeEnemy
{
    ghast,
    zombie,
    skeleton,
    enderman,
    thewither,
    enderdragon
}