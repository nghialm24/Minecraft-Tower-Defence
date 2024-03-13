using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Funzilla;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithController : MonoBehaviour
{
    private int bshLevel;
    [SerializeField] private List<GameObject> listBshLevel;
    [SerializeField] private CollectedItem stoneVip;
    [SerializeField] private Transform pos1;
    [SerializeField] private Text nameBuilding;
    
    [SerializeField] private GameObject upgrade1;
    [SerializeField] private GameObject effectCom;
    [SerializeField] private GameObject effectUpgr;
    [SerializeField] private Transform wareHouse;
    [SerializeField] private List<Slot> listSlot;
    [SerializeField] private List<Transform> listItem;
    private void Start()
    {
        foreach (var t in Profile.ListSaveBuilding.ToList())
        {
            if (t.indexSlot == GetComponent<SaveBuilding>().index)
            {
                bshLevel = (int) t.currentLevel - 1;
                upgrade1.SetActive(false);
                UpdateBlackSmith();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBlackSmith()
    {
        if (bshLevel < 1)
        {
            effectCom.SetActive(true);
            effectUpgr.SetActive(false);
        }
        else
        {
            effectUpgr.SetActive(true);
            effectCom.SetActive(false);
        }
        if (bshLevel < 1)
        {
            foreach (var t in listBshLevel)
            {
                t.SetActive(false);
            }
            listBshLevel[bshLevel].SetActive(true);
            bshLevel++;
        }
        if (bshLevel == 1) nameBuilding.transform.position += new Vector3(0, 0, 4.5f);

        nameBuilding.text = "Black Smith House";
        Profile.SaveBuilding(GetComponent<SaveBuilding>().index, bshLevel);

    }

    public void ProduceStoneVip()
    {
        var sA = Instantiate(stoneVip, transform.position, Quaternion.identity, wareHouse);
        sA.Init(CollectedItem.TypeItem.stoneVip);
        listItem.Clear();
        Resort();
    }

    private void Resort()
    {
        for (int i = 0; i < wareHouse.childCount; i++)
        {
            listItem.Add(wareHouse.GetChild(i));
            listItem[i].position = listSlot[i].transform.position;
        }
    }
}
