using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;
using Image = UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private Animator anim;
    private bool delay;
    private List<Slot> _listSlot;
    [SerializeField] private List<CollectedItem> listBlock;
    public bool isFull;
    public enum PlayerState
    {
        Idle,
        Run,
        Farm,
        Attack,
        Die
    }
    private PlayerState _currentState;

    private void EnterState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Run:
                Run();
                break;
            case PlayerState.Farm:
                Farm();
                break;
            case PlayerState.Attack:
                Attack();
                break;
            case PlayerState.Die:
                Die();
                break;
        }
    }

    private void ExitState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Run:
                break;
            case PlayerState.Farm:
                delay = false;
                break;
        }
    }

    private void UpdateState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                if(_joystick.Direction.x == 0 && _joystick.Direction.y == 0)
                    return;
                ChangeState(PlayerState.Run);
                break;
            case PlayerState.Run:
                Movement();
                break;
            case PlayerState.Farm:
                if (_joystick.Direction.x <= 0.01f && _joystick.Direction.x >= -0.01f &&
                    _joystick.Direction.y <= 0.01f && _joystick.Direction.y >= -0.01f)
                    delay = true;
                if (delay)
                    if (_joystick.Direction.x >= 0.7f || _joystick.Direction.x <= -0.7f ||
                        _joystick.Direction.y >= 0.7f || _joystick.Direction.y <= -0.7f)
                        ChangeState(PlayerState.Run);
                break;
        }
    }
    
    public void ChangeState(PlayerState newState)
    {
        if (newState == _currentState)
            return;
        ExitState();
        _currentState = newState;
        EnterState();

    }

    private void Update()
    {
        UpdateState();
    }

    private void Idle()
    {
        anim.SetTrigger("Idle");
    }

    private void Run()
    {
        anim.SetTrigger("Run");
        foreach (var slot in _listSlot)
        {
            slot.isBusy = false;
        }

        for (int i = 0; i < listBlock.Count; i++)
        {
            _listSlot[i].isBusy = true;
        }
    }
    
    private void Farm()
    {
    }
    
    private void Attack()
    {
        
    }
    private void Die()
    {
        
    }

    private void Start()
    {
        _listSlot = GetComponentsInChildren<Slot>().ToList();
    }

    private void Movement()
    {
        if(!_joystick.gameObject.activeSelf)
            return;
        var xMovement = _joystick.Direction.x;
        var zMovement = _joystick.Direction.y;

        Vector3 direction = Vector3.RotateTowards(transform.forward, new Vector3(xMovement,0,zMovement), 6 * Time.deltaTime,0.0f);
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position += new Vector3(xMovement, 0, zMovement)*10*Time.deltaTime;
        
        if(_joystick.Direction.x == 0 && _joystick.Direction.y == 0 && _currentState!= PlayerState.Farm)
            ChangeState(PlayerState.Idle);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StoneMineral"))
        {
            transform.LookAt(other.transform);
            ChangeState(PlayerState.Farm);
            TypeFarm(0);
        }
        
        if (other.CompareTag("Tree"))
        {
            transform.LookAt(other.transform);
            ChangeState(PlayerState.Farm);
            TypeFarm(1);
        }
        
        if (other.CompareTag("Cow"))
        {
            transform.LookAt(other.transform);
            ChangeState(PlayerState.Farm);
            TypeFarm(0);
        }
    }
    
    private void TypeFarm(int index)
    {
        if(index == 0)
        {
            anim.SetTrigger("Mining");
        }
        else if(index == 1)
        {
            anim.SetTrigger("Chopping");
        }
    }

    public void Stack(CollectedItem block)
    {
        foreach (var s in _listSlot)
        {
            if (_listSlot[^1].isBusy)
            {
                isFull = true;
                return;
            }

            if (s.isBusy) continue;
            block.transform.DOMove(s.transform.position, 0.2f).OnComplete(() =>
            {
                block.transform.parent = transform;
                block.transform.position = s.transform.position;
                block.transform.localEulerAngles = Vector3.zero;
                listBlock.Add(block);
            });
            s.isBusy = true;
            break;
        }
    }

    public void RemoveBlock(CollectedItem.TypeItem type, Transform pos)
    {
        foreach (var block in listBlock.Where(block => block._typeItem == type))
        {
            listBlock.Remove(block);
            block.transform.DOMove(pos.position, 0.2f).OnComplete(() =>
            {
                block.gameObject.SetActive(false);
                ReSort();
            });
            break;
        }
    }

    private void ReSort()
    {
        for (int i = 0; i < listBlock.Count; i++)
        {
            listBlock[i].transform.position = _listSlot[i].transform.position;
        }
    }
}
