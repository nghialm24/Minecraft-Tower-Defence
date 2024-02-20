using System;
using System.Collections;
using System.Collections.Generic;
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

    public enum TypeItem
    {
        woodVip,
        woodVip2,
        stoneVip
    }
    
    [SerializeField] private TypeItem typeProduct;

    private void Start()
    {
        //currentTime = cdTime;
        timeDelay = delay;
    }

    private void CheckProduce()
    {
        foreach (var ing in listIngredient)
        {
            switch (typeProduct)
            {
                case TypeItem.woodVip:
                    fusionHouseController.ProduceWoodVip();
                    ing.amount = ing.current;
                    ing.UpdateAmount();
                    break;
                case TypeItem.woodVip2:
                    fusionHouseController.ProduceWoodVip2();
                    ing.amount = ing.current;
                    ing.UpdateAmount();
                    break;
                case TypeItem.stoneVip:
                    blackSmithController.ProduceStoneVip();
                    ing.amount = ing.current;
                    ing.UpdateAmount();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            break;
        }
    }

    private void Update()
    {
        if (!locking)
            return;
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
            canvas.gameObject.SetActive(false);
            GetComponent<BoxCollider>().enabled = true;
            currentTime = cdTime;
            locking = false;
            SoundManager.PlaySfx("hit_ground");
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var ing in listIngredient)
            {
                if (ing.amount > 0)
                {
                    switch (ing.type)
                    {
                        case Ingredient.TypeItem.wood:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.wood) >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    other.GetComponent<PlayerController>()
                                        .RemoveBlock(CollectedItem.TypeItem.wood, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
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
                                        .RemoveBlock(CollectedItem.TypeItem.stone, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
                                    SoundManager.PlaySfx("drop2building");
                                }
                            }

                            break;
                        case Ingredient.TypeItem.skin:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.skin) >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    other.GetComponent<PlayerController>()
                                        .RemoveBlock(CollectedItem.TypeItem.skin, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
                                    SoundManager.PlaySfx("drop2building");
                                }
                            }

                            break;
                        case Ingredient.TypeItem.iron:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.iron) >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    other.GetComponent<PlayerController>()
                                        .RemoveBlock(CollectedItem.TypeItem.iron, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
                                    SoundManager.PlaySfx("drop2building");
                                }
                            }

                            break;
                        case Ingredient.TypeItem.diamond:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.diamond) >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    other.GetComponent<PlayerController>()
                                        .RemoveBlock(CollectedItem.TypeItem.diamond, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
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
                                        .RemoveBlock(CollectedItem.TypeItem.woodVip, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
                                    SoundManager.PlaySfx("drop2building");
                                }
                            }

                            break;
                        case Ingredient.TypeItem.stoneVip:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.stoneVip) >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    other.GetComponent<PlayerController>()
                                        .RemoveBlock(CollectedItem.TypeItem.stoneVip, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
                                    SoundManager.PlaySfx("drop2building");
                                }
                            }

                            break;
                        case Ingredient.TypeItem.woodVip2:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.woodVip2) >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    other.GetComponent<PlayerController>()
                                        .RemoveBlock(CollectedItem.TypeItem.woodVip2, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
                                    SoundManager.PlaySfx("drop2building");
                                }
                            }

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    canvas.gameObject.SetActive(true);
                    GetComponent<BoxCollider>().enabled = false;
                    locking = true;
                }
            }
        }
    }
}
