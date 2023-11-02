using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Funzilla;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] private Transform wareSlot;
    [SerializeField] private List<Slot> listSlot;
    [SerializeField] private List<CollectedItem> listWood;
    public bool isFull;
    private float delayCollect;
    [SerializeField] private GameObject upgrade1;

    private void Awake()
    {
   
    }

    private void Start()
    {
        foreach (var t in Profile.ListSaveBuilding)
        {
            if (t.indexSlot == GetComponent<SaveBuilding>().index)
            {
                beLevel = (int) t.currentLevel-1;
                upgrade1.SetActive(false);
                UpdateBe();
            }
        }
        listTree1 = forest1.GetComponentsInChildren<TreeController>().ToList();
        listTree2 = forest2.GetComponentsInChildren<TreeController>().ToList();        
        listPos1 = forest1.GetComponentsInChildren<TreeController>().ToList();
        listPos2 = forest2.GetComponentsInChildren<TreeController>().ToList();
        listSlot = wareSlot.GetComponentsInChildren<Slot>().ToList();
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
        if (!isFull)
            return;
        if (listWood.Count < listSlot.Count)
            isFull = false;
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
        
        Profile.SaveBuilding(GetComponent<SaveBuilding>().index, beLevel);
    }
    
    public void BornTree(int id, Transform pos)
    {
        if (id == 1)
        {
            var tree = Instantiate(Tree);
            tree.transform.parent = forest1;
            tree.transform.position = pos.transform.position;
            listTree1.Add(tree);
            tree.Init(1,this);
        }
        
        if (id == 2)
        {
            var tree = Instantiate(Tree);
            tree.transform.parent = forest2;
            tree.transform.position = pos.transform.position;
            listTree2.Add(tree);
            tree.Init(2,this);
        }
    }

    public void CollectWood(CollectedItem wood)
    {
        if (isFull) return;
        foreach (var t in listSlot)
        {
            if (listSlot[^1].isBusy)
                isFull = true;
            if (t.isBusy) continue;
            var w = Instantiate(wood);
            w.Init(CollectedItem.TypeItem.wood);
            w.haveAnim = false;
            w.transform.position = t.transform.position;
            w.GetComponent<BoxCollider>().enabled = false;
            listWood.Add(w);
            t.isBusy = true;
            break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<PlayerController>().isFull)
            {
                if(listWood.Count > 0)
                {
                    if (delayCollect > 0)
                        delayCollect -= Time.deltaTime;
                    else
                    {
                        other.GetComponent<PlayerController>().Stack(listWood[^1]);
                        listWood.Remove(listWood[^1]);
                        listSlot[listWood.Count].isBusy = false;
                        delayCollect = 0.05f;
                    }
                }
            }
        }
    }
}
