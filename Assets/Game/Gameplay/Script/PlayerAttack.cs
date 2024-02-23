using System;
using System.Collections;
using System.Collections.Generic;
using Funzilla;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private DataConfig _dataConfig => Gameplay.Instance.dataConfigSO.DataConfig;
    private SphereCollider colliderAttack;
    [SerializeField] private EnemyController target;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform rangeObj;
    private float delayAtk;
    private float distance;
    private float damage;
    private float atkSpeed;
    private float range;
    private void Awake()
    {
        colliderAttack = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        damage = _dataConfig.playerDamage;
        atkSpeed = _dataConfig.playerAtkSpeed;
        range = _dataConfig.playerRange;
        rangeObj.localScale = Vector3.one*range*2;
        colliderAttack.radius = range;
    }

    // Update is called once per frame
    public void Proc()
    {
        if(delayAtk > 0)
            delayAtk -= Time.deltaTime;
        if (target != null)
        {
            distance = Vector3.Distance(target.transform.position, transform.position);
            Debug.Log(distance);

            if (distance > range)
            {
                Debug.Log("out");
                colliderAttack.enabled = true;
                playerController.canAttack = false;
                target = null;
            }
            else
            {
                if(delayAtk <= 0)
                    Attack(target.transform);
            }
        }
        else
        {
            playerController.canAttack = false;
            if(colliderAttack.enabled)
                return;
            colliderAttack.enabled = true;
        }
    }
    
    private void Attack(Transform tg)
    {
        Debug.Log("attack");
        var b1 = Instantiate(_dataConfig.bullet, transform.position, Quaternion.identity);
        b1.GetComponent<Bullet>().Init(1,damage, tg);
        delayAtk = atkSpeed;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            colliderAttack.enabled = false;
            playerController.canAttack = true;
            target = other.GetComponent<EnemyController>();
            Debug.Log("ênmy");
        }
    }
}