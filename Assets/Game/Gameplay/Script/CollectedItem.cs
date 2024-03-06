using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Funzilla;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectedItem : MonoBehaviour
{
    public Animator anim;
    private Vector3 target;
    private Transform slot;
    public bool haveAnim;
    public bool fly;
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
        anim = GetComponent<Animator>();
        anim.Play("Instantiate");
        var x = Random.Range(1, 4);
        if(x == 1)
            target = transform.position + new Vector3(Random.Range(-5f,5f), 0, -5);
        if(x == 2)
            target = transform.position + new Vector3(Random.Range(-5f,5f), 0, 5);
        if(x == 3)
            target = transform.position + new Vector3(-5f, 0, Random.Range(-5f,5f));
        if(x == 4)
            target = transform.position + new Vector3(5, 0, Random.Range(-5f,5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (!haveAnim)
        {
            if(fly)
            {
                if (slot == null)
                    return;
                transform.position = Vector3.Lerp(transform.position, slot.transform.position, Time.deltaTime * 10);
            }
            return;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.x,0.5f,target.z), Time.deltaTime*2);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            haveAnim = false;
            if (other.GetComponent<PlayerController>().isFull) return;
            other.GetComponent<PlayerController>().collecting = 1f;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            //other.GetComponent<PlayerController>().Stack(this);
            slot = other.GetComponent<PlayerController>().ReturnSlot(this);
            anim.Play("Fly");
            PlaySound();
        }
    }

    public void StartFly()
    {
        fly = true;
    }
    public void EndFly()
    {
        fly = false;
        transform.localEulerAngles = Vector3.zero;
    }

    private void PlaySound()
    {
        switch (_typeItem)
        {
           case TypeItem.diamond:
               SoundManager.PlaySfx("diamond_collect");
               break;
           case TypeItem.iron:
               SoundManager.PlaySfx("iron_collect");
               break; 
           case TypeItem.stone:
               SoundManager.PlaySfx("rock_collect");
               break; 
           case TypeItem.wood:
               SoundManager.PlaySfx("wood_collect");
               break; 
           case TypeItem.woodVip:
               SoundManager.PlaySfx("wood_collect");
               break; 
           case TypeItem.woodVip2:
               SoundManager.PlaySfx("wood_collect");
               break;
           case TypeItem.skin:
               SoundManager.PlaySfx("wood_collect");
               break;
           case TypeItem.stoneVip:
               SoundManager.PlaySfx("rock_collect");
               break;
        }
    }
}