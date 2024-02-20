using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlaveController : MonoBehaviour
{
    private TreeController target;
    [SerializeField] private Animator anim;
    private Tween _tween1;
    [SerializeField] private BeController beController;
    private int level;
    private int indexTree;
    public enum PlayerState
    {
        Idle,
        Farm
    }
    private PlayerState _currentState;


    private void Start()
    {
        beController = BeController.Instance;
        Init(1, beController.listTree1[0], beController);
    }

    private void EnterState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                anim.SetTrigger("Idle");
                break;
            case PlayerState.Farm:
                Farm();
                break;
        }
    }
    
    private void ExitState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Farm:
                break;
        }
    }
    
    private void UpdateState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                if (!beController.isFull)
                {
                    ChangeState(PlayerState.Farm);
                }
                break;
            case PlayerState.Farm:
                if (beController.isFull)
                {
                    ChangeState(PlayerState.Idle);
                    return;
                }
                if (target == null)
                    return;
                if (!target.isDead)
                    return;
                Farm();
                break;
        }
    }
    
    private void ChangeState(PlayerState newState)
    {
        if (newState == _currentState)
            return;
        ExitState();
        _currentState = newState;
        EnterState();
    }
    
    public void Init(int lv, TreeController tg, BeController be)
    {
        level = lv;
        target = tg;
        //beController = be;
        Farm();
    }

    private void Awake()
    {
        beController = BeController.Instance;
    }

    private void Update()
    {
        UpdateState();
    }

    private void Farm()
    {
        switch (level)
        {
            case 1:
            {
                // if (beController.listTree1.Count == 0)
                //     beController.BornTree(1);
                // else target = beController.listTree1[0];
                if (target != null)
                {
                    if(indexTree < beController.listTree1.Count )
                    {
                        indexTree++;
                        target = beController.listTree1[indexTree];
                    }
                    else
                    {
                        indexTree = 0;
                        target = beController.listTree1[indexTree];
                    }
                }
                break;
            }
            case 2:
                // if (beController.listTree2.Count == 0)
                //     beController.BornTree(2);
                // else target = beController.listTree2[0];
                if (target != null)
                {
                    if(indexTree < beController.listTree2.Count )
                    {

                        indexTree++;
                        target = beController.listTree2[indexTree];
                    }
                    else
                    {
                        indexTree = 0;
                        target = beController.listTree2[indexTree];
                    }
                }
                break;
        }

        _tween1.Kill();
        _tween1 = transform.DOMove(target.transform.position, 5f);
        transform.LookAt(target.transform);
        anim.SetTrigger("Run");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Tree")) return;
        _tween1.Kill();
        target = other.GetComponent<TreeController>();
        anim.SetTrigger("Chopping");
    }
}
