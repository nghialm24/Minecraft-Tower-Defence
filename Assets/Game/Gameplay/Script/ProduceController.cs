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
            switch (typeProduct)
            {
                case TypeItem.woodVip:
                    fusionHouseController.ProduceWoodVip();
                    ing.amount = ing.current;
                    break;
                case TypeItem.ironVip:
                    fusionHouseController.ProduceIronVip();
                    ing.amount = ing.current;
                    break;
                case TypeItem.skinArmor:
                    blackSmithController.ProduceSkinArmor();
                    ing.amount = ing.current;
                    break;
                case TypeItem.ironArmor:
                    blackSmithController.ProduceIronArmor();
                    ing.amount = ing.current;
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
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.wood, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
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
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.stone, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
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
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.skin, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
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
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.iron, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
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
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.diamond, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
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
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.woodVip, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
                                }
                            }
                            break;                        
                        case Ingredient.TypeItem.ironVip:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.ironVip) >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.ironVip, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
                                }
                            }
                            break;                        
                        case Ingredient.TypeItem.skinArmor:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.skinArmor) >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.skinArmor, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
                                }
                            }
                            break;                        
                        case Ingredient.TypeItem.ironArmor:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.ironArmor) >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.ironArmor, transform);
                                    ing.amount -= 1;
                                    delay = timeDelay;
                                    ing.UpdateAmount();
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
