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
    [SerializeField] float delay2 = 2;
    public int id;
    [SerializeField] private bool quest3;
    [SerializeField] private bool quest6;
    [SerializeField] private bool quest8;
    [SerializeField] private bool quest10;
    public enum TypeBuilding
    {
        tower,
        be,
        bsh,
        fh
    } 

    [SerializeField] private TypeBuilding typeBuilding;

    private void Start()
    {
        if (Profile.Tutorial && !Tutorial.Instance.tutorial)
        {
            quest3 = false;
            quest6 = false;
            quest8 = false;
            quest10 = false;
        }
    }

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
                if (quest10)
                {
                    Tutorial.Instance.Quest10();
                    quest10 = false;
                }
                break;
            case TypeBuilding.be:
                beController.UpdateBe();
                break;
            case TypeBuilding.bsh:
                blackSmithController.UpdateBlackSmith();
                break;
            case TypeBuilding.fh:
                fusionHouseController.UpdateFusionHouse();
                if (quest8)
                {
                    Tutorial.Instance.Quest8();
                    quest8 = false;
                }
                break;
        }
        SoundManager.PlaySfx("building_upgrade");
    }

    private void CanBuildAll()
    {
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
                if (quest10)
                {
                    Tutorial.Instance.Quest10();
                    quest10 = false;
                }
                break;
            case TypeBuilding.be:
                beController.UpdateBe();
                break;
            case TypeBuilding.bsh:
                blackSmithController.UpdateBlackSmith();
                break;
            case TypeBuilding.fh:
                fusionHouseController.UpdateFusionHouse();
                if (quest8)
                {
                    Tutorial.Instance.Quest8();
                    quest8 = false;
                }
                break;
        }
        SoundManager.PlaySfx("building_upgrade");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().canBuildAll)
            {
                if (delay2 > 0)
                    delay2 -= Time.deltaTime;
                else
                {
                    CanBuildAll();
                    delay2 = 2;
                }
            }
            else
            for (int i = 0; i < listIngredient.Count; i++)
            {
                if (listIngredient[i].amount > 0)
                {
                    if (listIngredient[i].type == Ingredient.TypeItem.wood)
                    {
                        if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.wood) >= 1)
                        {
                            if (delay > 0)
                                delay -= Time.deltaTime;
                            else
                            {
                                other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.wood, transform);
                                listIngredient[i].amount -= 1;
                                delay = 0.3f;
                                listIngredient[i].UpdateAmount();
                                SoundManager.PlaySfx("drop2building");
                            }
                        }
                        else continue;
                    }
                    if (listIngredient[i].type == Ingredient.TypeItem.stone)
                    {
                        if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.stone) >= 1)
                        {
                            if (delay > 0)
                                delay -= Time.deltaTime;
                            else
                            {
                                other.GetComponent<PlayerController>()
                                    .RemoveBlock(CollectedItem.TypeItem.stone, transform);
                                listIngredient[i].amount -= 1;
                                delay = 0.3f;
                                listIngredient[i].UpdateAmount();
                                SoundManager.PlaySfx("drop2building");
                            }
                        }
                        else continue;
                    }
                    if (listIngredient[i].type == Ingredient.TypeItem.skin)
                    {
                        if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.skin) >= 1)
                        {
                            if (delay > 0)
                                delay -= Time.deltaTime;
                            else
                            {
                                other.GetComponent<PlayerController>()
                                    .RemoveBlock(CollectedItem.TypeItem.skin, transform);
                                listIngredient[i].amount -= 1;
                                delay = 0.3f;
                                listIngredient[i].UpdateAmount();
                                SoundManager.PlaySfx("drop2building");
                            }
                        }
                        else continue;
                    }
                    if (listIngredient[i].type == Ingredient.TypeItem.iron)
                    {
                        if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.iron) >= 1)
                        {
                            if (delay > 0)
                                delay -= Time.deltaTime;
                            else
                            {
                                other.GetComponent<PlayerController>()
                                    .RemoveBlock(CollectedItem.TypeItem.iron, transform);
                                listIngredient[i].amount -= 1;
                                delay = 0.3f;
                                listIngredient[i].UpdateAmount();
                                SoundManager.PlaySfx("drop2building");
                            }
                        }
                        else continue;
                    }
                    if (listIngredient[i].type == Ingredient.TypeItem.diamond)
                    {
                        if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.diamond) >= 1)
                        {
                            if (delay > 0)
                                delay -= Time.deltaTime;
                            else
                            {
                                other.GetComponent<PlayerController>()
                                    .RemoveBlock(CollectedItem.TypeItem.diamond, transform);
                                listIngredient[i].amount -= 1;
                                delay = 0.3f;
                                listIngredient[i].UpdateAmount();
                                SoundManager.PlaySfx("drop2building");
                            }
                        }
                        else continue;
                    }
                    if (listIngredient[i].type == Ingredient.TypeItem.woodVip)
                    {
                        if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.woodVip) >= 1)
                        {
                            if (delay > 0)
                                delay -= Time.deltaTime;
                            else
                            {
                                other.GetComponent<PlayerController>()
                                    .RemoveBlock(CollectedItem.TypeItem.woodVip, transform);
                                listIngredient[i].amount -= 1;
                                delay = 0.3f;
                                listIngredient[i].UpdateAmount();
                                SoundManager.PlaySfx("drop2building");
                            }
                        }
                        else continue;
                    }
                    if (listIngredient[i].type == Ingredient.TypeItem.stoneVip)
                    {
                        if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.stoneVip) >= 1)
                        {
                            if (delay > 0)
                                delay -= Time.deltaTime;
                            else
                            {
                                other.GetComponent<PlayerController>()
                                    .RemoveBlock(CollectedItem.TypeItem.stoneVip, transform);
                                listIngredient[i].amount -= 1;
                                delay = 0.3f;
                                listIngredient[i].UpdateAmount();
                                SoundManager.PlaySfx("drop2building");
                            }
                        }
                        else continue;
                    }
                    if (listIngredient[i].type == Ingredient.TypeItem.woodVip2)
                    {
                        if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.woodVip2) >= 1)
                        {
                            if (delay > 0)
                                delay -= Time.deltaTime;
                            else
                            {
                                other.GetComponent<PlayerController>()
                                    .RemoveBlock(CollectedItem.TypeItem.woodVip2, transform);
                                listIngredient[i].amount -= 1;
                                delay = 0.3f;
                                listIngredient[i].UpdateAmount();
                                SoundManager.PlaySfx("drop2building");
                            }
                        }
                        else continue;
                    }
                    break;
                }
                listIngredient[i].gameObject.SetActive(false);
                listIngredient.Remove(listIngredient[i]);
                CheckUpdate();
            }
        }
    }
}
/*
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
                        case Ingredient.TypeItem.stoneVip:
                            if (other.GetComponent<PlayerController>().CountItem(CollectedItem.TypeItem.stoneVip) >= 1)
                            {
                                if (delay > 0)
                                    delay -= Time.deltaTime;
                                else
                                {
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.stoneVip, transform);
                                    ing.amount -= 1;
                                    delay = 0.2f;
                                    ing.UpdateAmount();
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
                                    other.GetComponent<PlayerController>().RemoveBlock(CollectedItem.TypeItem.woodVip2, transform);
                                    ing.amount -= 1;
                                    delay = 0.2f;
                                    ing.UpdateAmount();
                                }
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
*/