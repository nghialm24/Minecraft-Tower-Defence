using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Funzilla;
using TMPro;
using UnityEngine;

public class UpgradesController : MonoBehaviour
{
    [SerializeField] private TowerController towerController;
    [SerializeField] private BeController beController;
    [SerializeField] private BlackSmithController blackSmithController;
    [SerializeField] private FusionHouseController fusionHouseController;
    [SerializeField] private List<Ingredient> listIngredient;
    [SerializeField] private TextMeshPro nameBuilding;
    [SerializeField] float delay;
    public int id;
    [SerializeField] private bool quest3;
    [SerializeField] private bool quest6;
    [SerializeField] private bool quest8;
    [SerializeField] private bool quest11;
    [SerializeField] private bool quest13;
    public enum TypeBuilding
    {
        tower,
        be,
        bsh,
        fh
    } 

    [SerializeField] private TypeBuilding typeBuilding;

    private void CheckUpdate()
    {
        if (listIngredient.Count != 0) return;
        gameObject.SetActive(false);
        switch (typeBuilding)
        {
            case TypeBuilding.tower:
                towerController.UpdateTower(id);
                if (quest3)
                {
                    Tutorial.Instance.Quest3();
                    quest3 = false;
                }
                if (quest6)
                {
                    Tutorial.Instance.Quest6();
                    quest6 = false;
                }

                if (quest13)
                {
                    Tutorial.Instance.Quest13();
                    quest13 = false;
                }
                break;
            case TypeBuilding.be:
                beController.UpdateBe();
                break;
            case TypeBuilding.bsh:
                blackSmithController.UpdateBlackSmith();
                if (quest8)
                {
                    Tutorial.Instance.Quest8();
                    quest8 = false;
                }
                break;
            case TypeBuilding.fh:
                fusionHouseController.UpdateFusionHouse();
                if (quest11)
                {
                    Tutorial.Instance.Quest11();
                    quest11 = false;
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
                                    delay = 0.2f;
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
                                    delay = 0.2f;
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
                                    delay = 0.2f;
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
                                    delay = 0.2f;
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
                                    delay = 0.2f;
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
                                    delay = 0.2f;
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
                                    delay = 0.2f;
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
                                    delay = 0.2f;
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
                                    delay = 0.2f;
                                    ing.UpdateAmount();
                                }
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    ing.gameObject.SetActive(false);
                    listIngredient.Remove(ing);
                    CheckUpdate();
                    break;
                }
            }
        }
    }
}
