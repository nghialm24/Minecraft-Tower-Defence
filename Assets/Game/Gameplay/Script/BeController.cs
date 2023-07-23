using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Funzilla;
using TMPro;
using UnityEngine;

public class BeController : MonoBehaviour
{
    [SerializeField] private SlaveController slave;
    [SerializeField] private TreeController Tree;
    [SerializeField] private Transform forest1;
    [SerializeField] private Transform forest2;
    public List<TreeController> listTree1;
    public List<TreeController> listTree2;
    private List<TreeController> listPos1;
    private List<TreeController> listPos2;
    [SerializeField] private TextMeshPro nameBuilding;
    public int beLevel;
    [SerializeField] private List<GameObject> listBeLevel;
    private void Start()
    {
        listTree1 = forest1.GetComponentsInChildren<TreeController>().ToList();
        listTree2 = forest2.GetComponentsInChildren<TreeController>().ToList();        
        listPos1 = forest1.GetComponentsInChildren<TreeController>().ToList();
        listPos2 = forest2.GetComponentsInChildren<TreeController>().ToList();
        foreach (var tree in listTree1)
        {
            tree.Init(1,this);
        }
        foreach (var tree in listTree2)
        {
            tree.Init(2,this);
        }
        // var s = Instantiate(slave);
        // s.transform.parent = Gameplay.Instance.transform;
        // s.transform.position = transform.position;
        // s.Init(listTree[0], this);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    
    
    public void UpdateBe()
    {
        if (beLevel < 2)
        {
            foreach (var t in listBeLevel)
            {
                t.SetActive(false);
            }
            listBeLevel[beLevel].SetActive(true);
            beLevel++;
            var s = Instantiate(slave);
            s.transform.parent = Gameplay.Instance.transform;
            s.transform.position = transform.position;
            if(beLevel == 1)
                s.Init(beLevel,listTree1[0], this);
            if(beLevel == 2)
                s.Init(beLevel,listTree2[0], this);
        }

        nameBuilding.text = "BE " + beLevel;
        nameBuilding.transform.position = transform.position + Vector3.forward*4;
    }
    
    public void BornTree(int id)
    {
        if (id == 1)
        {
            foreach (var pos in listPos1)
            {
                var tree = Instantiate(Tree);
                tree.transform.parent = forest1;
                tree.transform.position = pos.transform.position;
                listTree1.Add(tree);
                tree.Init(1,this);
            }
        }
        
        if (id == 2)
        {
            foreach (var pos in listPos2)
            {
                var tree = Instantiate(Tree);
                tree.transform.parent = forest2;
                tree.transform.position = pos.transform.position;
                listTree2.Add(tree);
                tree.Init(2,this);
            }
        }
    }
}
