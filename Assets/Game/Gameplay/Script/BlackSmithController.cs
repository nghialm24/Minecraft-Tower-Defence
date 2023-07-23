using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlackSmithController : MonoBehaviour
{
    private int bshLevel;
    [SerializeField] private List<GameObject> listBshLevel;
    [SerializeField] private CollectedItem skinArmor;
    [SerializeField] private CollectedItem ironArmor;
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
        nameBuilding.text = "Black Smith House " + bshLevel;
        nameBuilding.transform.position = transform.position + Vector3.forward*4;
    }

    public void ProduceWoodArmor()
    {
        var sA = Instantiate(skinArmor);
        sA.transform.position = pos1.position;
        sA.Init(CollectedItem.TypeItem.skinArmor);
    }

    public void ProduceIronArmor()
    {
        var iA = Instantiate(ironArmor);
        iA.transform.position = pos2.position;
        iA.Init(CollectedItem.TypeItem.ironArmor);
    }
}
