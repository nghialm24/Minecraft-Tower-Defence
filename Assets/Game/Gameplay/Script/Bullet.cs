using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private Transform target;
    private int id;
    [SerializeField] private Transform effectPar;
    public void Init(int i, float dmg, Transform tg,int idEff)
    {
        damage = dmg;
        target = tg;
        id = i;
        if(idEff>=effectPar.childCount)
            return;
        for (int j = 0; j < effectPar.childCount; j++)
        {
            effectPar.GetChild(i).gameObject.SetActive(false);
            if (j == idEff)
            {
                effectPar.GetChild(j).gameObject.SetActive(true);
            }
        }
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
            transform.LookAt(target);

        }else gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var e = other.GetComponent<EnemyController>();
            if (e.typeEnemy == TypeEnemy.thewither || e.typeEnemy == TypeEnemy.enderdragon)
            {
                AnimUI.Instance.BossProcess(damage);
            }
            e.hp -= damage;
            e.ShowEff();
            gameObject.SetActive(false);
        }
    }
}
