using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Funzilla;
using UnityEngine;
using UnityEngine.AI;
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
    [SerializeField] private BeController _beController;
    private int num;
    [SerializeField] private List<Transform> part;
    [SerializeField] private Transform model;
    public bool isDead;
    [SerializeField] private float reviveTime;
    [SerializeField] private GameObject tree;
    public void Init(int i, BeController be)
    {
        num = i;
        //_beController = be;
    }
    private void Start()
    {
        delay = timeDelay;
        delay2 = timeDelay;
        _beController = BeController.Instance;
    }

    // Update is called once per frame
    private void Update()
    {
        if(isDead)
            Revive();
        if(!farming)
            return;
        if(delay > 0)
            delay -= Time.deltaTime;
        else
        {
            if (countWood > 0)
            {
                countWood -= 1;
                AnimWood(part[countWood]);
                _beController.CollectWood(wood);
                transform.GetChild(0).transform.position += Vector3.down;
                delay = timeDelay;
            }
            else
            {
                farming = false;
                reviveTime = 10;
                isDead = true;
                transform.GetChild(0).gameObject.SetActive(false);
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<NavMeshObstacle>().enabled = false;
                // if(num == 1)
                //     _beController.listTree1.Remove(this);
                // if(num == 2)
                //     _beController.listTree2.Remove(this);
            }
        }
    }

    private void Revive()
    {
        if(reviveTime > 0)
            reviveTime -= Time.deltaTime;
        else
        {
            isDead = false;
            var t = Instantiate(tree);
            t.GetComponent<TreeController>().Init(num, _beController);
            t.transform.parent = Gameplay.Instance.transform;
            t.transform.position = transform.position;
            gameObject.SetActive(false);
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
                    reviveTime = 10;
                    isDead = true;
                    transform.GetChild(0).gameObject.SetActive(false);
                    GetComponent<BoxCollider>().enabled = false;
                    GetComponent<NavMeshObstacle>().enabled = false;
                    other.GetComponent<PlayerController>().ChangeState(PlayerController.PlayerState.Idle);
                    SoundManager.PlaySfx("axe_chop_wood");
                }
                if (countWood > 0)
                {
                    countWood -= 1;
                    AnimWood(part[countWood]);
                    AnimTree();
                    var w = Instantiate(wood,Gameplay.Instance.transform);
                    w.transform.position = transform.position;
                    w.Init(CollectedItem.TypeItem.wood);
                    //transform.GetChild(0).transform.position += Vector3.down*1.2f;
                    delay2 = timeDelay;
                    SoundManager.PlaySfx("axe_chop_wood");
                }
            }
        }
    }

    private void AnimWood(Transform tf)
    {
        tf.DOScale(Vector3.zero, 0.3f);
    }

    private void AnimTree()
    {
        model.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.3f)
            .OnComplete(() =>
            {
                model.DOScale(Vector3.one, 0.3f);
                
            });
    }
}
