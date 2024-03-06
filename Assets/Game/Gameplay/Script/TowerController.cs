using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Funzilla;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TowerController : MonoBehaviour
{
    private DataConfig _dataConfig => Gameplay.Instance.dataConfigSO.DataConfig;
    public int towerLevel;
    [SerializeField] private List<GameObject> listTowerLevel;
    [SerializeField] private List<UpgradesController> listUp4;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Text nameBuilding;
    [SerializeField] private EnemyController target;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject sprite;
    [SerializeField] private GameObject upgrade1;
    private float distance;
    private float damage;
    private float atkSpeed;
    private float delayAtk;
    private float range;
    private List<TowerData> _towerdata => _dataConfig.towerData;
    [SerializeField] private GameObject effectCom;
    [SerializeField] private GameObject effectUpgr;
    private void Start()
    {
        foreach (var t in Profile.ListSaveBuilding.ToList())
        {
            if (t.indexSlot == GetComponent<SaveBuilding>().index)
            {
                towerLevel = (int) t.currentLevel - 1;
                upgrade1.SetActive(false);
                UpdateTower(0);
            }
        }
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
        if(delayAtk > 0)
            delayAtk -= Time.deltaTime;
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
                if(delayAtk <= 0)
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

    private int idTowerLv4;
    public void UpdateTower(int id)
    {
        if (towerLevel < 1)
        {
            effectCom.SetActive(true);
            effectUpgr.SetActive(false);
        }
        else
        {
            effectUpgr.SetActive(true);
            effectCom.SetActive(false);
        }
        if (towerLevel < 3)
        {
            foreach (var t in listTowerLevel)
            {
                t.SetActive(false);
            }
            listTowerLevel[towerLevel].SetActive(true);
            towerLevel++;
            nameBuilding.text = "Tower " + towerLevel;
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
            canvas.position += new Vector3(0, 0, 2);
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
        Profile.SaveBuilding(GetComponent<SaveBuilding>().index, towerLevel);
        idTowerLv4 = id;
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
        if(towerLevel == 2)
        {
            var b1 = Instantiate(_dataConfig.bullet);
            //var b2 = Instantiate(_dataConfig.bullet);
            b1.transform.position = firePos.position;
            //b2.transform.position = firePos.position + new Vector3(0, 1, 0);

            //b.transform.DOMove(tg.transform.position + new Vector3(0, 0, -0.5f), 0.7f);
            b1.GetComponent<Bullet>().Init(damage, tg,3);
            //b2.GetComponent<Bullet>().Init(1,damage*0.5f, tg,3);
            delayAtk = atkSpeed;
        }
        else
        {
            var b = Instantiate(_dataConfig.bullet);
            b.transform.position = firePos.position;
            //b.transform.DOMove(tg.transform.position + new Vector3(0, 0, -0.5f), 0.7f);
            delayAtk = atkSpeed;
            if(towerLevel == 3)
                b.GetComponent<Bullet>().Init(damage, tg,4);
            else if (towerLevel == 1)
            {
                b.GetComponent<Bullet>().Init(damage, tg,0);
            }
            else
            {
                b.GetComponent<Bullet>().Init(damage, tg,5);
            }
        }
        SoundManager.PlaySfx("Tower_shot");
    }
}
