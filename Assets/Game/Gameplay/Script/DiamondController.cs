using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Funzilla;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    [SerializeField] private CollectedItem diamond;
    private float delay2;
    [SerializeField] private float timeDelay;
    void Start()
    {
        delay2 = timeDelay;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(delay2 > 0)
                delay2 -= Time.deltaTime;
            else
            {
                var w = Instantiate(diamond);
                w.transform.position = transform.position;
                //w.transform.DOMove(other.transform.position, 1f);
                w.Init(CollectedItem.TypeItem.diamond);
                delay2 = timeDelay;
            }
        }
    }
}
