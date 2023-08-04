using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Funzilla;
using UnityEngine;

public class IronController : MonoBehaviour
{
    [SerializeField] private CollectedItem iron;
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
                var w = Instantiate(iron);
                w.transform.position = transform.position;
                //w.transform.DOMove(other.transform.position, 1f);
                w.Init(CollectedItem.TypeItem.iron);
                delay2 = timeDelay;
            }
        }
    }
}
