using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public int towerLevel;
    [SerializeField] private List<GameObject> listTowerLevel;
    [SerializeField] private List<UpgradesController> listUp4;
    [SerializeField] private TextMeshPro nameBuilding;
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
        }
    }
}
