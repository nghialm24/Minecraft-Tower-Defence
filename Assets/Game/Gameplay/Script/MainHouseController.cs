using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHouseController : MonoBehaviour
{
    [SerializeField] private GameObject canvasHP;
    
    void Start()
    {
        canvasHP.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
