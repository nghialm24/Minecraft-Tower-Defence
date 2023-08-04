using System;
using System.Collections;
using System.Collections.Generic;
using Funzilla;
using UnityEngine;

public class ProduceController : MonoBehaviour
{
    [SerializeField] private List<Ingredient> listIngredient;
    [SerializeField] private BlackSmithController blackSmithController;
    [SerializeField] private FusionHouseController fusionHouseController;
    [SerializeField] private float delay;
    private float timeDelay;
    public enum TypeItem
    {
        woodVip,
        ironVip,
        skinArmor,
        ironArmor
    }
    
    [SerializeField] private TypeItem typeProduct;

    private void Start()
    {
        timeDelay = delay;
    }

    private void CheckProduce()
    {
        foreach (var ing in listIngredient)
        {
            if (ing.current < ing.amount)
                break;
            if (listIngredient[^1].current != listIngredient[^1].amount) continue;
            switch (typeProduct)
            {
                case TypeItem.woodVip:
                    fusionHouseController.ProduceWoodVip();
                    ing.current = 0;
                    break;
                case TypeItem.ironVip:
                    fusionHouseController.ProduceIronVip();
                    ing.current = 0;
                    break;
                case TypeItem.skinArmor:
                    blackSmithController.ProduceWoodArmor();
                    ing.current = 0;
                    break;
                case TypeItem.ironArmor:
                    blackSmithController.ProduceIronArmor();
                    ing.current = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            break;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var ing in listIngredient)
            {
                if (ing.current < ing.amount)
                {
                    switch (ing.type)
                    {
                        case Ingredient.TypeItem.wood:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.wood) >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    ing.current = ing.amount;
                                    delay = timeDelay;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.stone:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.stone) >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    ing.current = ing.amount;
                                    delay = timeDelay;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.skin:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.skin) >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    ing.current = ing.amount;
                                    delay = timeDelay;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.iron:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.iron) >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    ing.current = ing.amount;
                                    delay = timeDelay;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.diamond:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.diamond) >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    ing.current = ing.amount;
                                    delay = timeDelay;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.woodVip:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.woodVip) >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    ing.current = ing.amount;
                                    delay = timeDelay;
                                }
                            }
                            break;                        
                        case Ingredient.TypeItem.ironVip:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.ironVip) >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    ing.current = ing.amount;
                                    delay = timeDelay;
                                }
                            }
                            break;                        
                        case Ingredient.TypeItem.skinArmor:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.skinArmor) >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    ing.current = ing.amount;
                                    delay = timeDelay;
                                }
                            }
                            break;                        
                        case Ingredient.TypeItem.ironArmor:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.ironArmor) >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    ing.current = ing.amount;
                                    delay = timeDelay;
                                }
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else CheckProduce();
            }
        }
    }
}
