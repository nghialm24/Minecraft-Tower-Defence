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
    private int index;
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
        if(index < listIngredient.Count - 1)
        {
            if (listIngredient[index].amount != 0) return;
            index++;
        }
        else
        {
            gameObject.SetActive(false);
            switch (typeBuilding)
            {
                case TypeBuilding.tower:
                    towerController.UpdateTower(id);
                    break;
                case TypeBuilding.be:
                    beController.UpdateBe();
                    break;
                case TypeBuilding.bsh:
                    blackSmithController.UpdateBlackSmith();
                    break;
                case TypeBuilding.fh:
                    fusionHouseController.UpdateFusionHouse();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            index = 0;
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
                            if (Gameplay.Instance.itemCollection.wood >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.wood -= 1;
                                    Gameplay.Instance.itemCollection.UpdateWood();
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.wood, transform);
                                    ing.amount -= 1;
                                    delay = 0.2f;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.stone:
                            if (Gameplay.Instance.itemCollection.stone >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.stone -= 1;
                                    Gameplay.Instance.itemCollection.UpdateStone();
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.stone, transform);
                                    ing.amount -= 1;
                                    delay = 0.2f;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.skin:
                            if (Gameplay.Instance.itemCollection.skin >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.skin -= 1;
                                    Gameplay.Instance.itemCollection.UpdateSkin();
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.skin, transform);
                                    ing.amount -= 1;
                                    delay = 0.2f;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.iron:
                            if (Gameplay.Instance.itemCollection.iron >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.iron -= 1;
                                    Gameplay.Instance.itemCollection.UpdateIron();
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.iron, transform);
                                    ing.amount -= 1;
                                    delay = 0.2f;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.diamond:
                            if (Gameplay.Instance.itemCollection.diamond >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.diamond -= 1;
                                    Gameplay.Instance.itemCollection.UpdateDiamond();
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.diamond, transform);
                                    ing.amount -= 1;
                                    delay = 0.2f;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.woodVip:
                            if (Gameplay.Instance.itemCollection.woodVip >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.woodVip -= 1;
                                    Gameplay.Instance.itemCollection.UpdateWoodVip();
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.woodVip, transform);
                                    ing.amount -= 1;
                                    delay = 0.2f;
                                }
                            }
                            break;                        
                        case Ingredient.TypeItem.ironVip:
                            if (Gameplay.Instance.itemCollection.ironVip >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.ironVip -= 1;
                                    Gameplay.Instance.itemCollection.UpdateIronVip();
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.ironVip, transform);

                                    ing.amount -= 1;
                                    delay = 0.2f;
                                }
                            }
                            break;                        
                        case Ingredient.TypeItem.skinArmor:
                            if (Gameplay.Instance.itemCollection.skinArmor >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.skinArmor -= 1;
                                    Gameplay.Instance.itemCollection.UpdateSkinArmor();
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.skinArmor, transform);

                                    ing.amount -= 1;
                                    delay = 0.2f;
                                }
                            }
                            break;                        
                        case Ingredient.TypeItem.ironArmor:
                            if (Gameplay.Instance.itemCollection.ironArmor >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.ironArmor -= 1;
                                    Gameplay.Instance.itemCollection.UpdateIronArmor();
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.ironArmor, transform);
                                    ing.amount -= 1;
                                    delay = 0.2f;
                                }
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    CheckUpdate();
                    if (ing.gameObject.activeSelf)
                    {
                        ing.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
