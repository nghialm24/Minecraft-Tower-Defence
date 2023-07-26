using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Funzilla;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TowerController : MonoBehaviour
{
    private DataConfig _dataConfig => Gameplay.Instance.dataConfigSO.DataConfig;
    public int towerLevel;
    [SerializeField] private List<GameObject> listTowerLevel;
    [SerializeField] private List<UpgradesController> listUp4;
    [SerializeField] private TextMeshPro nameBuilding;
    [SerializeField] private EnemyController target;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject sprite;
    private float distance;
    private int damage;
    private float atkSpeed;
    private float delayAtk;
    private float range;
    private List<TowerData> _towerdata => _dataConfig.towerData;
    private void Start()
    {
        for (int i = 0; i < listUp4.Count; i++)
        {
            listUp4[i].id = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(towerLevel == 0 )
            return;
        if (target != null)
        {
            distance = Vector3.Distance(target.transform.position, firePos.position);
            if (distance > range+3)
            {
                GetComponent<SphereCollider>().enabled = true;
                target = null;
            }
            else
            {
                Attack(target.transform);
            }
        }
        else
        {
            if(GetComponent<SphereCollider>().enabled)
                return;
            GetComponent<SphereCollider>().enabled = true;
        }
    }

    public void UpdateTower(int id)
    {
        if (towerLevel < 3)
        {
            foreach (var t in listTowerLevel)
            {
                t.SetActive(false);
            }
            listTowerLevel[towerLevel].SetActive(true);
            towerLevel++;
            nameBuilding.text = "Tower " + towerLevel;
            nameBuilding.transform.position = transform.position + Vector3.forward*4;
            var tD = _towerdata.FirstOrDefault(x => x.level == towerLevel);
            GetComponent<SphereCollider>().enabled = true;
            if (tD == null) return;
            damage = tD.damage;
            atkSpeed = tD.atkSpeed;
            delayAtk = atkSpeed;
            GetComponent<SphereCollider>().radius = tD.range;
            range = tD.range;
            sprite.gameObject.SetActive(true);
            sprite.transform.localScale = new Vector3(tD.range*2,tD.range*2,tD.range*2);
        }
        else
        {
            foreach (var t in listTowerLevel)
            {
                t.SetActive(false);
            }
            listTowerLevel[towerLevel].SetActive(true);
            listTowerLevel[towerLevel].transform.GetChild(id).gameObject.SetActive(true);
            towerLevel++;
            nameBuilding.text = "Tower " + towerLevel;
            nameBuilding.transform.position = transform.position + Vector3.forward*4;
            var tD = _towerdata.FirstOrDefault(x => x.level == towerLevel);
            GetComponent<SphereCollider>().enabled = true;
            if (tD == null) return;
            damage = tD.damage;
            atkSpeed = tD.atkSpeed;
            delayAtk = atkSpeed;
            GetComponent<SphereCollider>().radius = tD.range;
            range = tD.range;
            sprite.gameObject.SetActive(true);
            sprite.transform.localScale = new Vector3(tD.range*2,tD.range*2,tD.range*2);
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GetComponent<SphereCollider>().enabled = false;
            target = other.GetComponent<EnemyController>();
        }
    }

    private void Attack(Transform tg)
    {
        if(delayAtk > 0)
            delayAtk -= Time.deltaTime;
        else
        {
            var b = Instantiate(_dataConfig.bullet);
            b.transform.position = firePos.position;
            //b.transform.DOMove(tg.transform.position + new Vector3(0, 0, -0.5f), 0.7f);
            b.GetComponent<Bullet>().Init(damage,tg);
            delayAtk = atkSpeed;
        }
    }
}
