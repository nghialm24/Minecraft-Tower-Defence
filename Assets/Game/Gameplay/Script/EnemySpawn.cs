using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using Funzilla;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private EnemyController enemy;
    [SerializeField] List<SplineComputer> listSplineComputer;
    public List<EnemyController> listEnemy1;
    public List<EnemyController> listEnemy2;
    [SerializeField] private float countDownSpawn;
    [SerializeField] private float countDownPerWay;
    [SerializeField] private bool isx2;
    private int currentWave;
    [SerializeField] private List<int> wave;
    void Start()
    {
        //SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (wave[currentWave] > 0)
        {
            if(isx2)
            {
                if (countDownSpawn > 0)
                    countDownSpawn -= Time.deltaTime;
                else
                {
                    countDownSpawn = 5;
                    SpawnEnemy1();
                    SpawnEnemy2();
                    wave[currentWave] -= 1;
                }
            }
            else
            {
                if (countDownSpawn > 0)
                    countDownSpawn -= Time.deltaTime;
                else
                {
                    countDownSpawn = 5;
                    SpawnEnemy1();
                    wave[currentWave] -= 1;
                }
            }
        }
        else
        {
            if(currentWave < wave.Count - 1)
            {
                if (countDownPerWay > 0)
                    countDownPerWay -= Time.deltaTime;
                else
                {
                    currentWave++;
                    countDownPerWay = 15;
                }
            }else Gameplay.Instance.Win();
        }
    }

    private void SpawnEnemy1()
    {
        var e = Instantiate(enemy);
        e.transform.parent = transform;
        e.transform.position = transform.position;
        e.Init(listSplineComputer[0]);
        listEnemy1.Add(e);
    }    
    
    private void SpawnEnemy2()
    {
        var e = Instantiate(enemy);
        e.transform.parent = transform;
        e.transform.position = transform.position;
        e.Init(listSplineComputer[1]);
        listEnemy2.Add(e);
    }
}
