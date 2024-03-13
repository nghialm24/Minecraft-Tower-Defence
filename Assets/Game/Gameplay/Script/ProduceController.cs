using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Funzilla;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProduceController : MonoBehaviour
{
    [SerializeField] private List<Ingredient> listIngredient;
    [SerializeField] private BlackSmithController blackSmithController;
    [SerializeField] private FusionHouseController fusionHouseController;
    [SerializeField] private float delay;
    private float timeDelay;
    [SerializeField] private Image img;
    [SerializeField] private GameObject canvas;
    private bool locking;
    [SerializeField] private float cdTime;
    [SerializeField] private float currentTime;
    [SerializeField] private float timeSound = 1f;
    [SerializeField] private Transform bag;
    [SerializeField] private List<Slot> listSlot;
    private int stock;
    public bool stop;
    public enum TypeItem
    {
        woodVip,
        woodVip2,
        stoneVip
    }
    
    [SerializeField] private TypeItem typeProduct;

    private void Start()
    {
        timeDelay = delay;
    }

    private void CheckProduce()
    {
        switch (typeProduct)
        {
            case TypeItem.woodVip:
                fusionHouseController.ProduceWoodVip();
                break;
            case TypeItem.woodVip2:
                fusionHouseController.ProduceWoodVip2();
                break;
            case TypeItem.stoneVip:
                blackSmithController.ProduceStoneVip();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Update()
    {
        if (stop) return;
        if (stock < listIngredient[0].amount) return;
        // if (!locking)
        //     return;
        CountDown();
    }

    private void CountDown()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            img.fillAmount = currentTime / cdTime;
            if (timeSound > 0)
            {
                timeSound -= Time.deltaTime;
            }
            else
            {
                SoundManager.PlaySfx("produce");
                timeSound = 1f;
            }
        }      
        else
        {
            CheckProduce();
            MinusStock();
            stock -= listIngredient[0].amount;
            GetComponent<BoxCollider>().enabled = true;
            currentTime = cdTime;
            locking = false;
            SoundManager.PlaySfx("hit_ground");
        }
    }

    private void MinusStock()
    {
        switch (typeProduct)
        {
            case TypeItem.woodVip:
                for (int i = 0; i < listIngredient[0].amount; i++)
                {
                    listSlot[stock-1-i].transform.GetChild(0).gameObject.SetActive(false);
                }
                break;
            case TypeItem.woodVip2:
                for (int i = 0; i < listIngredient[0].amount; i++)
                {
                    listSlot[stock-1-i].transform.GetChild(2).gameObject.SetActive(false);
                }
                break;
            case TypeItem.stoneVip:
                for (int i = 0; i < listIngredient[0].amount; i++)
                {
                    listSlot[stock-1-i].transform.GetChild(1).gameObject.SetActive(false);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().collecting > 0) return;
            if (stock >= 16) return;
            switch (listIngredient[0].type)
                {
                    case Ingredient.TypeItem.wood:
                        if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.wood) >= 1)
                        {
                            if (delay > 0)
                                delay -= Time.deltaTime;
                            else
                            {
                                other.GetComponent<PlayerController>()
                                    .RemoveBlock(CollectedItem.TypeItem.wood, listSlot[stock].transform);
                                listSlot[stock].transform.GetChild(0).gameObject.SetActive(true);
                                stock++;
                                delay = timeDelay;
                                SoundManager.PlaySfx("drop2building");
                            }
                        }
        
                        break;
                    case Ingredient.TypeItem.stone:
                        if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.stone) >= 1)
                        {
                            if (delay > 0)
                                delay -= Time.deltaTime;
                            else
                            {
                                other.GetComponent<PlayerController>()
                                    .RemoveBlock(CollectedItem.TypeItem.stone, listSlot[stock].transform);
                                listSlot[stock].transform.GetChild(1).gameObject.SetActive(true);
                                stock++;
                                delay = timeDelay;
                                SoundManager.PlaySfx("drop2building");
                            }
                        }
        
                        break;
                    case Ingredient.TypeItem.woodVip:
                        if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.woodVip) >= 1)
                        {
                            if (delay > 0)
                                delay -= Time.deltaTime;
                            else
                            {
                                other.GetComponent<PlayerController>()
                                    .RemoveBlock(CollectedItem.TypeItem.woodVip, listSlot[stock].transform);
                                listSlot[stock].transform.GetChild(2).gameObject.SetActive(true);
                                stock++;
                                delay = timeDelay;
                                SoundManager.PlaySfx("drop2building");
                            }
                        }
                        
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        // foreach (var ing in listIngredient)
        // {
        //     if (ing.amount > stock)
        //     {
        //         
        //     }
        //     else
        //     {
        //         canvas.gameObject.SetActive(true);
        //         GetComponent<BoxCollider>().enabled = false;
        //         locking = true;
        //     }
        // }
        }
    }
}
