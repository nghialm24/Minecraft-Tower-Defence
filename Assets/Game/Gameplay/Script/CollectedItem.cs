using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Funzilla;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectedItem : MonoBehaviour
{
    [SerializeField] private float anim;
    private Vector3 target;
    public bool haveAnim;
    public enum TypeItem
    {
        stone,
        wood,
        skin,
        iron,
        diamond,
        woodVip,
        stoneVip,
        woodVip2
    }

    public TypeItem _typeItem;
    public void Init(TypeItem type)
    {
        _typeItem = type;
    }

    private void Start()
    {
        var x = Random.Range(1, 4);
        if(x == 1)
            target = transform.position + new Vector3(Random.Range(-4f,4f), 0, -4);
        if(x == 2)
            target = transform.position + new Vector3(Random.Range(-4f,4f), 0, 4);
        if(x == 3)
            target = transform.position + new Vector3(-4f, 0, Random.Range(-4f,4f));
        if(x == 4)
            target = transform.position + new Vector3(4, 0, Random.Range(-4f,4f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!haveAnim) return;
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.x,0.5f,target.z), Time.deltaTime*2);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            haveAnim = false;
            if (other.GetComponent<PlayerController>().isFull) return;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<PlayerController>().Stack(this);
        }
    }
}
