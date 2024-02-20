using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Funzilla;
using UnityEngine;
using Random = UnityEngine.Random;

public class CowController : MonoBehaviour
{
    [SerializeField] private CollectedItem cowSkin;
    private Transform cage;
    [SerializeField] private int count;
    private bool run;
    [SerializeField] float speed;
    [SerializeField] private Animator anim;
    private float timeAuto;
    private float t;
    [SerializeField] private AllCowController _allCowController;
    private void Start()
    {
        t = Random.Range(1, 10);
        timeAuto = 15 + t;
        cage = _allCowController.transform;
    }

    public void Init(Transform c, AllCowController allc)
    {
        cage = c;
        _allCowController = allc;
    }
    void Update()
    {
        if (!run)
        {
            if(timeAuto > -5)
            {
                timeAuto -= Time.deltaTime;
                if (timeAuto < 0)
                {
                    transform.position += t/(t+1) * transform.forward * Time.deltaTime;
                    if (timeAuto < 0 && timeAuto > -0.1f)
                    {
                        anim.SetTrigger("walk");
                    }
                }
            }
            else
            {
                AutoMove();
                t = Random.Range(1, 10);
                timeAuto = 15 + t;
            }
        }
        else
        {
            if (speed > 0)
            {
                speed -= Time.deltaTime;
                transform.position += transform.forward * Time.deltaTime * 5f;
                GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                anim.SetTrigger("eat");
                GetComponent<BoxCollider>().enabled = true;
                run = false;
            }
        }
    }

    private void AutoMove()
    {
        anim.SetTrigger("eat");
        if (transform.position.x > cage.position.x + 7 
            || transform.position.x < cage.position.x - 7
            || transform.position.z > cage.position.z + 7
            || transform.position.z < cage.position.z - 7)
        {
            transform.eulerAngles += new Vector3(0,180,0);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(count == 1)
            {
                other.GetComponent<PlayerController>().ChangeState(PlayerController.PlayerState.Idle);
                gameObject.SetActive(false);
                _allCowController.BornCow();
            }
            
            if (count <= 0) return;
            anim.SetTrigger("run");
            var w = Instantiate(cowSkin,Gameplay.Instance.transform);
            w.transform.position = transform.position;
            w.Init(CollectedItem.TypeItem.skin);
            count -= 1;
            run = true;
            speed = 1;
        }
    }
}
