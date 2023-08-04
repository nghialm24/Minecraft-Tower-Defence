using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Funzilla;
using UnityEngine;

public class CowController : MonoBehaviour
{
    [SerializeField] private CollectedItem cowSkin;
    //private float delay2;
    //[SerializeField] private float timeDelay;
    [SerializeField] private int count;
    private bool run;
    [SerializeField] float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!run)
            return;
        if (speed > 0)
        {
            speed -= Time.deltaTime;
            GetComponent<Rigidbody>().velocity = transform.forward * 5;
            Debug.Log("Runnn");
        }
        else
        {
            GetComponent<Rigidbody>().velocity = transform.forward * 0;
            run = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(count == 1)
            {
                gameObject.SetActive(false);
            }

            if (count <= 0) return;
            var w = Instantiate(cowSkin);
            w.transform.position = transform.position;
            w.Init(CollectedItem.TypeItem.skin);
            count -= 1;
            run = true;
            speed = 1;
        }
    }
}
