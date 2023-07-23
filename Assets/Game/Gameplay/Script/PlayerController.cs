using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
}
