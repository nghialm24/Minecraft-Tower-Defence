using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    private Transform target;
    public void Init(int dmg, Transform tg)
    {
        damage = dmg;
        target = tg;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if(!target.gameObject.activeSelf)
                gameObject.SetActive(false);
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime*5);
        }else gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().hp -= damage;
            gameObject.SetActive(false);
        }
    }
}
