using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Funzilla;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeController : MonoBehaviour
{
    [SerializeField] private int countWood;
    private float delay;
    private float delay2;
    [SerializeField] private float timeDelay;
    private bool farming;
    [SerializeField] private CollectedItem wood;
    private GameObject target;
    private BeController _beController;
    private int num;
    public void Init(int i, BeController be)
    {
        num = i;
        _beController = be;
    }
    private void Start()
    {
        delay = timeDelay;
        delay2 = timeDelay;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!farming)
            return;
        if(delay > 0)
            delay -= Time.deltaTime;
        else
        {
            if (countWood > 0)
            {
                countWood -= 1;
                _beController.CollectWood(wood);
                transform.GetChild(0).transform.position += Vector3.down;
                delay = timeDelay;
            }
            else
            {
                farming = false;
                gameObject.SetActive(false);
                if(num == 1)
                    _beController.listTree1.Remove(this);
                if(num == 2)
                    _beController.listTree2.Remove(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slave"))
        {
            farming = true;
            target = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(delay2 > 0)
                delay2 -= Time.deltaTime;
            else
            {
                if(countWood == 1)
                {
                    gameObject.SetActive(false);
                    if(num == 1)
                        _beController.listTree1.Remove(this);
                    if(num == 2)
                        _beController.listTree2.Remove(this);                    
                }
                if (countWood > 0)
                {
                    countWood -= 1;
                    var w = Instantiate(wood,Gameplay.Instance.transform);
                    w.transform.position = transform.position;
                    w.Init(CollectedItem.TypeItem.wood);
                    transform.GetChild(0).transform.position += Vector3.down*1.2f;
                    delay2 = timeDelay;
                }
            }
        }
    }
}
