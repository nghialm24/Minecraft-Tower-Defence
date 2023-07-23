using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Funzilla;
using UnityEngine;

public class CowController : MonoBehaviour
{
    [SerializeField] private CollectedItem cowSkin;
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
                var w = Instantiate(cowSkin);
                w.transform.position = transform.position;
                w.Init(CollectedItem.TypeItem.skin);
                delay2 = timeDelay;
                gameObject.SetActive(false);
            }
        }
    }
}
