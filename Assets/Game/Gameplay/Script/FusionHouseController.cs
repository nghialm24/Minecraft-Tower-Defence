using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FusionHouseController : MonoBehaviour
{
    private int fhLevel;
    [SerializeField] private List<GameObject> listFhLevel;
    [SerializeField] private CollectedItem woodVip;
    [SerializeField] private CollectedItem ironVip;
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private TextMeshPro nameBuilding;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFusionHouse()
    {
        if (fhLevel < 2)
        {
            foreach (var t in listFhLevel)
            {
                t.SetActive(false);
            }
            listFhLevel[fhLevel].SetActive(true);
            fhLevel++;
        }

        nameBuilding.text = "Fusion House " + fhLevel;
        nameBuilding.transform.position = transform.position + Vector3.forward*4;
    }
    
    public void ProduceWoodVip()
    {
        var wV = Instantiate(woodVip);
        wV.transform.position = pos1.position;
        wV.Init(CollectedItem.TypeItem.woodVip);
    }

    public void ProduceIronVip()
    {
        var wI = Instantiate(ironVip);
        wI.transform.position = pos2.position;
        wI.Init(CollectedItem.TypeItem.ironVip);
    }
}
