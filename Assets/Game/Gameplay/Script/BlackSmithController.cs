using System.Collections;
using System.Collections.Generic;
using Funzilla;
using TMPro;
using UnityEngine;

public class BlackSmithController : MonoBehaviour
{
    private int bshLevel;
    [SerializeField] private List<GameObject> listBshLevel;
    [SerializeField] private CollectedItem stoneVip;
    [SerializeField] private Transform pos1;
    [SerializeField] private TextMeshPro nameBuilding;
    
    [SerializeField] private GameObject upgrade1;

    private void Start()
    {
        foreach (var t in Profile.ListSaveBuilding)
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
        if (bshLevel < 2)
        {
            foreach (var t in listBshLevel)
            {
                t.SetActive(false);
            }
            listBshLevel[bshLevel].SetActive(true);
            bshLevel++;
        }
        nameBuilding.text = "Black Smith House";
        nameBuilding.transform.position = transform.position + Vector3.forward*4;
        
        Profile.SaveBuilding(GetComponent<SaveBuilding>().index, bshLevel);

    }

    public void ProduceStoneVip()
    {
        var sA = Instantiate(stoneVip);
        sA.transform.position = pos1.position;
        sA.Init(CollectedItem.TypeItem.stoneVip);
    }
}
