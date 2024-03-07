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
    private float delayCollider;
    
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
            if (distance > range)
            {
                playerController.canAttack = false;
            }
            else
            {
                playerController.transform.LookAt(target.transform);
                if(delayAtk <= 0)
                    Attack(target.transform);
            }
        }
        else
        {
            ResetTarget();
        }
    }

    public void ResetTarget()
    {
        colliderAttack.enabled = true;
        playerController.canAttack = false;
        target = null;
    }
    private void Attack(Transform tg)
    {
        var b1 = Instantiate(_dataConfig.bullet, transform.position+new Vector3(0,2,0), Quaternion.identity);
        b1.GetComponent<Bullet>().Init(damage, tg,6);
        delayAtk = atkSpeed;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            target = other.GetComponent<EnemyController>();
            colliderAttack.enabled = false;
            playerController.canAttack = true;
        }
    }
}
