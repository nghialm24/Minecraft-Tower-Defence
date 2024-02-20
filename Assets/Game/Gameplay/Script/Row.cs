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
                splineFollower.followSpeed = enemyConfigs[0].moveSpeed;
                hp = enemyConfigs[0].hp;
                break;
            case TypeEnemy.zombie:
                splineFollower.followSpeed = enemyConfigs[1].moveSpeed;
                hp = enemyConfigs[1].hp;
                break;
            case TypeEnemy.skeleton:
                splineFollower.followSpeed = enemyConfigs[2].moveSpeed;
                hp = enemyConfigs[2].hp;
                break;
            case TypeEnemy.enderman:
                splineFollower.followSpeed = enemyConfigs[3].moveSpeed;
                hp = enemyConfigs[3].hp;
                break;
            case TypeEnemy.thewither:
                splineFollower.followSpeed = enemyConfigs[4].moveSpeed;
                hp = enemyConfigs[4].hp;
                break;
            case TypeEnemy.enderdragon:
                splineFollower.followSpeed = enemyConfigs[5].moveSpeed;
                hp = enemyConfigs[5].hp;
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
        LevelProcess.Instance.EnemyProcess();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
