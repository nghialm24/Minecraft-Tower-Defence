using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dreamteck.Splines;
using Funzilla;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn Instance;
    private DataConfig dataConfig => Gameplay.Instance.dataConfigSO.DataConfig;
    [SerializeField] private Row row;
    [SerializeField] List<SplineComputer> listSplineComputer;
    [SerializeField] private List<Row> listRow;
    [SerializeField] private List<Row> listRow2;
    [SerializeField] private bool isx2;
    [SerializeField] private GameObject start;
    [SerializeField] private bool startSpawn;
    private int round;
    private float delaySpawn;
    private float delaySpawn2;
    private int index;
    private int index2;
    private bool isSpawn;
    private bool isSpawn2;
    [SerializeField] private bool quest4;
    [SerializeField] private bool quest7;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (Profile.Tutorial && !Tutorial.Instance.tutorial)
        {
            quest4 = false;
            quest7 = false;
        }

        round = 2;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!startSpawn) return;
        if (!isSpawn)
        {
            if(!isx2)
            {

                if (listRow.Count <= 0 && index != 0)
                {
                    SetupNextRound();
                }
            }
            else
            {
                if (listRow.Count <= 0 && index != 0 && listRow2.Count <= 0 && index2 != 0)
                {
                    SetupNextRound();
                }
            }
        }
        else
        {
            if(!isx2)
            {
                if (delaySpawn > 0)
                    delaySpawn -= Time.deltaTime;
                else
                {
                    SpawnEnemy1();
                }
            }
            else
            {
                if(isSpawn)
                {
                    if (delaySpawn > 0)
                        delaySpawn -= Time.deltaTime;
                    else
                    {
                        SpawnEnemy1();
                    }
                }

                if (isSpawn2)
                {
                    if (delaySpawn2 > 0)
                        delaySpawn2 -= Time.deltaTime;
                    else
                    {
                        SpawnEnemy2();
                    }
                }
            }
        }
    } 

    private void SpawnEnemy1()
    {
        var r = Instantiate(row,transform.position,Quaternion.identity);
        r.transform.parent = Gameplay.Instance.transform;
        listRow.Add(r);
        var e = dataConfig.worldData[Profile.Level - 1].levelData[round].listEnemyDelay[index];
        r.Init(listSplineComputer[0], e.typeEnemy, e.trans, this, 1);
        if (index < dataConfig.worldData[Profile.Level - 1].levelData[round].listEnemyDelay.Count - 1)
        {
            index++;
        }
        else
        {
            index = 1;
            isSpawn = false;
        }
        delaySpawn = e.timeDelay-2;
    }
    
    private void SpawnEnemy2()
    {
        var r = Instantiate(row,transform.position,Quaternion.identity);
        r.transform.parent = Gameplay.Instance.transform;
        listRow2.Add(r);
        var e = dataConfig.worldData[Profile.Level].levelData[round].listEnemyDelay[index2];
        r.Init(listSplineComputer[1], e.typeEnemy, e.trans, this, 2);
        if (index2 < dataConfig.worldData[Profile.Level].levelData[round].listEnemyDelay.Count - 1)
        {
            index2++;
        }
        else
        {
            index2 = 1;
            isSpawn2 = false;
        }
        delaySpawn2 = e.timeDelay-2;
    }

    public void SetupSpawn()
    {
        startSpawn = true;
        SoundManager.PlaySfx("roar1");
        start.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        isSpawn = true;
        isSpawn2 = true;
        index = 0;
        index2 = 0;
        Debug.Log("spawn");
        //round = dataConfig.worldData[Profile.Level-1].levelData.Count;
        if(!isx2)
        {
            if (dataConfig.worldData[Profile.Level - 1].levelData.Count != 0)
            {
                delaySpawn = 1;
            }
            //LevelProcess.Instance.SetEnemyAmount(dataConfig.worldData[Profile.Level - 1].levelData[round].listEnemyDelay.Count);
        }
        else
        {
            if (dataConfig.worldData[Profile.Level - 1].levelData.Count != 0)
            {
                delaySpawn = 1;
            }
            if (dataConfig.worldData[Profile.Level].levelData.Count != 0)
            {
                delaySpawn = 1;
            }
            //LevelProcess.Instance.SetEnemyAmount(dataConfig.worldData[Profile.Level].levelData[round].listEnemyDelay.Count+dataConfig.worldData[Profile.Level - 1].levelData[round].listEnemyDelay.Count);
        }
    }

    private void SetupNextRound()
    {
        if (quest4)
        {
            if (round == 0)
            {
                Tutorial.Instance.Quest4();
                quest4 = false;
            }
        }
        
        if(quest7)
        {
            if (round == 1)
            {
                Tutorial.Instance.Quest7();
                quest7 = false;
            }
        }
        startSpawn = false;
        if (Gameplay.Instance.isGameOver)
            return;
        start.SetActive(true);
        SoundManager.PlaySfx("success");
        GetComponent<BoxCollider>().enabled = true;
        round++;
        index = 0;
        index2 = 0;
        Profile.CurrentRound = round;
        Debug.Log(Profile.CurrentRound);
        if(round >=  dataConfig.worldData[Profile.Level - 1].levelData.Count)
        {
            Profile.CurrentRound = 0;
            Gameplay.Instance.Win();
            SoundManager.PlaySfx("level-win");
        }
    }

    public void RemoveFormList(Row r, int id)
    {
        if(id == 1)
            listRow.Remove(r);
        else if(id == 2) listRow2.Remove(r);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerController>().start.gameObject.SetActive(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerController>().start.gameObject.SetActive(false);
    }
}
