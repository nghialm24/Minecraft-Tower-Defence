using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Funzilla;
using UnityEngine;
using UnityEngine.UI;

public class FusionHouseController : MonoBehaviour
{
    private int fhLevel;
    [SerializeField] private List<GameObject> listFhLevel;
    [SerializeField] private CollectedItem woodVip;
    [SerializeField] private CollectedItem woodVip2;
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Text nameBuilding;
    [SerializeField] private GameObject upgrade1;
    [SerializeField] private GameObject effectCom;
    [SerializeField] private GameObject effectUpgr;
    
    [SerializeField] private Transform wareHouse1;
    [SerializeField] private List<Slot> listSlot1;
    [SerializeField] private List<Transform> listItem1;
    
    [SerializeField] private Transform wareHouse2;
    [SerializeField] private List<Slot> listSlot2;
    [SerializeField] private List<Transform> listItem2;
    private void Start()
    {
        foreach (var t in Profile.ListSaveBuilding.ToList())
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
        if (fhLevel < 1)
        {
            effectCom.SetActive(true);
            effectUpgr.SetActive(false);
        }
        else
        {
            effectUpgr.SetActive(true);
            effectCom.SetActive(false);
        }
        if (fhLevel < 2)
        {
            foreach (var t in listFhLevel)
            {
                t.SetActive(false);
            }
            listFhLevel[fhLevel].SetActive(true);
            fhLevel++;
        }
        if (fhLevel == 2) nameBuilding.transform.position += new Vector3(0, 0, 4.5f);

        nameBuilding.text = "Fusion House " + fhLevel;
        Profile.SaveBuilding(GetComponent<SaveBuilding>().index, fhLevel);
    }
    
    public void ProduceWoodVip()
    {
        var wV = Instantiate(woodVip, transform.position, Quaternion.identity, wareHouse1);
        wV.Init(CollectedItem.TypeItem.woodVip);
        listItem1.Clear();
        Resort1();
    }

    public void ProduceWoodVip2()
    {
        var wI = Instantiate(woodVip2, transform.position, Quaternion.identity, wareHouse2);
        wI.transform.position = pos2.position;
        wI.Init(CollectedItem.TypeItem.woodVip2);
        listItem2.Clear();
        Resort2();
    }
    
    private void Resort1()
    {
        for (int i = 0; i < wareHouse1.childCount; i++)
        {
            listItem1.Add(wareHouse1.GetChild(i));
            listItem1[i].position = listSlot1[i].transform.position;
        }
    }    
    
    private void Resort2()
    {
        for (int i = 0; i < wareHouse2.childCount; i++)
        {
            listItem2.Add(wareHouse2.GetChild(i));
            listItem2[i].position = listSlot2[i].transform.position;
        }
    }
}
