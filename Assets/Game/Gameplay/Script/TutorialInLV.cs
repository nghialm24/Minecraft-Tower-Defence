using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInLV : MonoBehaviour
{
    [SerializeField] private GameObject other;
    public GameObject start;
    // public Transform tree;
    // public Transform tree2;
    // public Transform tree3;
    public List<Transform> pos;
    public GameObject allTower;
    public GameObject bsh;
    public GameObject fsh;
    public List<GameObject> allUpgrades;
    private void Start()
    {
        Tutorial.Instance.Init(start, other, pos, allTower, bsh, fsh, allUpgrades);
    }

    void Update()
    {
        
    }
}
