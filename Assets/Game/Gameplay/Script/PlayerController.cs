using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DG.Tweening;
using Funzilla;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Debug = UnityEngine.Debug;
using Image = UnityEngine.UI.Image;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator uiAnim;
    private bool delay;
    private List<Slot> listSlot;
    public List<CollectedItem> listBlock;
    //public bool isFull;
    [SerializeField] private Text current;
    [SerializeField] private Text amount;
    [SerializeField] private Tutorial Tuto;
    public bool tutorial;
    [SerializeField] private GameObject axe1;
    [SerializeField] private GameObject axe2;
    [SerializeField] private GameObject bow;
    public bool canBuildAll;
    public GameObject start;
    private float delayFootStep;
    [SerializeField] private SphereCollider playerCollider;
    public bool canAttack;
    [SerializeField] private Transform bag;
    public float collecting = 0.5f;
    public int stock;
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
            case PlayerState.Attack:
                break;
        }
    }

    private void UpdateState()
    {
        switch (_currentState)
        {
            case PlayerState.Idle:
                if(canAttack) ChangeState(PlayerState.Attack);
                if(_joystick.Direction.x == 0 && _joystick.Direction.y == 0)
                    return;
                ChangeState(PlayerState.Run);
                break;
            case PlayerState.Run:
                Movement();
                if (delayFootStep > 0)
                    delayFootStep -= Time.deltaTime;
                else
                {
                    SoundManager.PlaySfx("footstep01");
                    delayFootStep = 0.35f;
                }
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
            case PlayerState.Attack:
                playerAttack.Proc();
                if(!canAttack)
                    ChangeState(PlayerState.Idle);
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
        if(collecting > 0)
            collecting -= Time.deltaTime;
    }

    private void Idle()
    {     
        playerAttack.ResetTarget();
        playerCollider.isTrigger = true;
        anim.SetTrigger("Idle");
    }

    private void Run()
    {
        //bag.gameObject.SetActive(true);
        playerCollider.isTrigger = false;
        //playerAttack.ResetTarget();
        anim.SetTrigger("Run");

        uiAnim.Play(listBlock.Count >= 23 && listBlock.Count != 0 ? "UI_On" : "UI_Off");
    }
    
    private void Farm()
    {
        anim.SetTrigger("Mining");
        uiAnim.Play("UI_On");
    }
    
    private void Attack()
    {
        //bag.gameObject.SetActive(false);
        anim.SetTrigger("Attack");
        ChangeBow(true);
        //uiAnim.Play("UI_Off");
    }
    private void Die()
    {
        
    }

    private void Start()
    {
        playerCollider = GetComponent<SphereCollider>();
        listSlot = GetComponentsInChildren<Slot>().ToList();
        amount.text = "/" + listSlot.Count;
        current.text = listBlock.Count.ToString();
        delayFootStep = 0.1f;
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
        {
            ChangeState(PlayerState.Idle);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StoneMineral"))
        {
            transform.LookAt(other.transform);
            ChangeState(PlayerState.Farm);
            TypeFarm(0);
            ChangeAxe(false);
            ChangeBow(false);
        }
        
        if (other.CompareTag("Tree"))
        {
            transform.LookAt(other.transform);
            ChangeState(PlayerState.Farm);
            TypeFarm(1);
            ChangeAxe(true);
            ChangeBow(false);
        }
        
        if (other.CompareTag("Cow"))
        {
            transform.LookAt(other.transform);
            ChangeState(PlayerState.Farm);
            TypeFarm(0);
            ChangeAxe(false);
            ChangeBow(false);
        }
        
        if (other.CompareTag("Sell"))
        {
            uiAnim.Play("UI_On");
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Sell"))
        {
            if(listBlock.Count > 0)
            {
                RemoveBlock(listBlock[0]._typeItem, other.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // if (other.CompareTag("Upgrade"))
        // {
        //     colliderAttack.enabled = true;
        //     Debug.Log("true");
        // }
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
        // if (stock >= 24) return;
        // foreach (var s in listSlot)
        // {
        //     if (listSlot[^1].isBusy)
        //     {
        //         isFull = true;
        //     }else
        //     {
        //         if (s.isBusy) continue;
        //         break;
        //     }
        // }
    }

    public Transform ReturnSlot(CollectedItem block)
    {
        Transform temp = null;
        block.transform.position = listSlot[stock].transform.position; 
        block.transform.parent = bag;
        block.transform.localEulerAngles = Vector3.zero;
        listBlock.Add(block);
        stock++;
        current.text = stock.ToString();
        if (tutorial)
        {
            Tuto.UpItem(block._typeItem);
        }
        return temp;
    }
    
    public void RemoveBlock(CollectedItem.TypeItem type, Transform pos)
    {
        foreach (var block in listBlock.Where(block => block._typeItem == type))
        {
            block.anim.Play("Collected");
            block.transform.DOMove(pos.position, 0.3f).OnComplete(() =>
            {
                //block.gameObject.SetActive(false);
                //block.transform.
                listBlock.Remove(block);
                ReSort();
            });
            current.text = listBlock.Count.ToString();
            break;
        }
    }

    private void ReSort()
    {
        for (int i = 0; i < listBlock.Count; i++)
        {
            listBlock[i].transform.position = listSlot[i].transform.position;
            // if (i + 1 > listBlock.Count)
            // {
            //     stock = listBlock.Count;
            // }

        }
        stock = listBlock.Count;
        current.text = listBlock.Count.ToString();
        //if (listBlock.Count < listSlot.Count)
            //isFull = false;
    }

    public int CountItem(CollectedItem.TypeItem type)
    {
        var countItem = 0;
        foreach (var item in listBlock)
        {
            if (item._typeItem == type)
            {
                countItem++;
            }
        }

        return countItem;
    }

    private void ChangeAxe(bool isTree)
    {
        axe1.SetActive(isTree);
        axe2.SetActive(!isTree);
    }

    private void ChangeBow(bool isBow)
    {
        axe1.SetActive(!isBow);
        axe2.SetActive(!isBow);
        bow.SetActive(isBow);
    }
    public void Max(Text text)
    {
        canBuildAll = !canBuildAll;
        text.text = canBuildAll.ToString();
    }

    public void StartRound()
    {
        EnemySpawn.Instance.SetupSpawn();
        start.gameObject.SetActive(false);
    }

    public void CancelRunAnim()
    {
        _joystick.OnPointerUp(null);
        ChangeState(PlayerState.Idle);
    }
}
