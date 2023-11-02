using System.Collections;
using System.Collections.Generic;
using Funzilla;
using TMPro;
using UnityEngine;

public class FusionHouseController : MonoBehaviour
{
    private int fhLevel;
    [SerializeField] private List<GameObject> listFhLevel;
    [SerializeField] private CollectedItem woodVip;
    [SerializeField] private CollectedItem woodVip2;
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private TextMeshPro nameBuilding;
    [SerializeField] private GameObject upgrade1;

    private void Start()
    {
        foreach (var t in Profile.ListSaveBuilding)
        {
            if (t.indexSlot == GetComponent<SaveBuilding>().index)
            {
                fhLevel = (int) t.currentLevel - 1;
                upgrade1.SetActive(false);
                UpdateFusionHouse();
            }
        }
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
        
        Profile.SaveBuilding(GetComponent<SaveBuilding>().index, fhLevel);
    }
    
    public void ProduceWoodVip()
    {
        var wV = Instantiate(woodVip);
        wV.transform.position = pos1.position;
        wV.Init(CollectedItem.TypeItem.woodVip);
    }

    public void ProduceWoodVip2()
    {
        var wI = Instantiate(woodVip2);
        wI.transform.position = pos2.position;
        wI.Init(CollectedItem.TypeItem.woodVip2);
    }
}
