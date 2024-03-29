using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Funzilla;
using UnityEngine;

public class StoneController : MonoBehaviour
{
    [SerializeField] private CollectedItem stone;
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
                var w = Instantiate(stone,Gameplay.Instance.transform);
                
                w.transform.position = transform.position;
                //w.transform.DOMove(other.transform.position, 1f);
                w.Init(CollectedItem.TypeItem.stone);
                delay2 = timeDelay;
            }
        }
    }
}
