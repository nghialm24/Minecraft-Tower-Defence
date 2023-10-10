using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using System.Linq;
using Funzilla;
using UnityEngine;
using Random = System.Random;

public class Row : MonoBehaviour
{
    private DataEnemy dataEnemy => Gameplay.Instance.dataEnemySO.DataEnemy;
    private List<EnemyConfig> enemyConfigs => dataEnemy.listEnemyConfig;
    [SerializeField] private EnemyController enemy;
    [SerializeField] private EnemySpawn enemySpawn;
    private SplineFollower splineFollower;
    private int idRow;
    private float hp;
    private void Start()
    {
        
    }

    public void Init(SplineComputer splineComputer, TypeEnemy typeEnemy, float x, EnemySpawn eSpawn, int id)
    {
        splineFollower = GetComponent<SplineFollower>();
        splineFollower.spline = splineComputer;
        splineFollower.enabled = true;
        switch (typeEnemy)
        {
            case TypeEnemy.ghast:
                splineFollower.followSpeed = 2;
                hp = 1;
                break;
            case TypeEnemy.zombie:
                splineFollower.followSpeed = 2;
                hp = 2;
                break;
            case TypeEnemy.skeleton:
                splineFollower.followSpeed = 2f;
                hp = 3;
                break;
            case TypeEnemy.enderman:
                splineFollower.followSpeed = 2;
                hp = 5;
                break;
            case TypeEnemy.thewither:
                splineFollower.followSpeed = 2;
                hp = 20;
                break;
            case TypeEnemy.enderdragon:
                splineFollower.followSpeed = 2;
                hp = 4;
                break;
        }
        enemy.Init(typeEnemy, x, hp);
        enemySpawn = eSpawn;
        idRow = id;
    }

    public void Destroy()
    {
        Destroy(gameObject);
        enemySpawn.RemoveFormList(this, idRow);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
