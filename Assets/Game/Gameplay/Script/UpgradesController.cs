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
        foreach (var ing in listIngredient)
        {
            if (ing.current < ing.amount)
                break;
            if (listIngredient[^1].current != listIngredient[^1].amount) continue;
            gameObject.SetActive(false);
            switch (typeBuilding)
            {
                case TypeBuilding.tower:
                    // if (towerController.towerLevel == 3)
                    // {
                    //     towerController.UpdateTower(id);
                    // }
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
                            if (Gameplay.Instance.itemCollection.wood >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.wood -= ing.amount;
                                    Gameplay.Instance.itemCollection.UpdateWood();
                                    ing.current = ing.amount;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.stone:
                            if (Gameplay.Instance.itemCollection.stone >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.stone -= ing.amount;
                                    Gameplay.Instance.itemCollection.UpdateStone();
                                    ing.current = ing.amount;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.skin:
                            if (Gameplay.Instance.itemCollection.skin >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.skin -= ing.amount;
                                    Gameplay.Instance.itemCollection.UpdateSkin();
                                    ing.current = ing.amount;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.iron:
                            if (Gameplay.Instance.itemCollection.iron >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.iron -= ing.amount;
                                    Gameplay.Instance.itemCollection.UpdateIron();
                                    ing.current = ing.amount;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.diamond:
                            if (Gameplay.Instance.itemCollection.diamond >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.diamond -= ing.amount;
                                    Gameplay.Instance.itemCollection.UpdateDiamond();
                                    ing.current = ing.amount;
                                }
                            }
                            break;
                        case Ingredient.TypeItem.woodVip:
                            if (Gameplay.Instance.itemCollection.woodVip >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.woodVip -= ing.amount;
                                    Gameplay.Instance.itemCollection.UpdateWoodVip();
                                    ing.current = ing.amount;
                                }
                            }
                            break;                        
                        case Ingredient.TypeItem.ironVip:
                            if (Gameplay.Instance.itemCollection.ironVip >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.ironVip -= ing.amount;
                                    Gameplay.Instance.itemCollection.UpdateIronVip();
                                    ing.current = ing.amount;
                                }
                            }
                            break;                        
                        case Ingredient.TypeItem.skinArmor:
                            if (Gameplay.Instance.itemCollection.skinArmor >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.skinArmor -= ing.amount;
                                    Gameplay.Instance.itemCollection.UpdateSkinArmor();
                                    ing.current = ing.amount;
                                }
                            }
                            break;                        
                        case Ingredient.TypeItem.ironArmor:
                            if (Gameplay.Instance.itemCollection.ironArmor >= ing.amount)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    Gameplay.Instance.itemCollection.ironArmor -= ing.amount;
                                    Gameplay.Instance.itemCollection.UpdateIronArmor();
                                    ing.current = ing.amount;
                                }
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else CheckUpdate();
            }
        }
    }
}
