using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private Transform target;
    private int id;
    public void Init(int i, float dmg, Transform tg)
    {
        damage = dmg;
        target = tg;
        id = i;
    }
    void Start()
    {
        transform.GetChild(id).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if(!target.gameObject.activeSelf)
                gameObject.SetActive(false);
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime*25);
        }else gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var e = other.GetComponent<EnemyController>();
            if (e.typeEnemy == TypeEnemy.thewither || e.typeEnemy == TypeEnemy.enderdragon)
            {
                LevelProcess.Instance.BossProcess(damage);
            }
            e.hp -= damage;
            gameObject.SetActive(false);
        }
    }
}
