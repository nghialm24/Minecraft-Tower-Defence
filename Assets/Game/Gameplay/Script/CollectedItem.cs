using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Funzilla;
using UnityEngine;

public class CollectedItem : MonoBehaviour
{
    public enum TypeItem
    {
        stone,
        wood,
        skin,
        iron,
        diamond,
        woodVip,
        ironVip,
        skinArmor,
        ironArmor
    }

    private TypeItem _typeItem;
    public void Init(TypeItem type)
    {
        _typeItem = type;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (_typeItem)
            {
                case TypeItem.stone:
                    Gameplay.Instance.itemCollection.stone++;
                    Gameplay.Instance.itemCollection.UpdateStone();
                    break;
                case TypeItem.wood:
                    Gameplay.Instance.itemCollection.wood++;
                    Gameplay.Instance.itemCollection.UpdateWood();                    
                    break;
                case TypeItem.skin:
                    Gameplay.Instance.itemCollection.skin++;
                    Gameplay.Instance.itemCollection.UpdateSkin();
                    break;
                case TypeItem.iron:
                    Gameplay.Instance.itemCollection.iron++;
                    Gameplay.Instance.itemCollection.UpdateIron();
                    break;
                case TypeItem.diamond:
                    Gameplay.Instance.itemCollection.diamond++;
                    Gameplay.Instance.itemCollection.UpdateDiamond();
                    break;
                case TypeItem.woodVip:
                    Gameplay.Instance.itemCollection.woodVip++;
                    Gameplay.Instance.itemCollection.UpdateWoodVip();
                    break;
                case TypeItem.ironVip:
                    Gameplay.Instance.itemCollection.ironVip++;
                    Gameplay.Instance.itemCollection.UpdateIronVip();                    
                    break;
                case TypeItem.skinArmor:
                    Gameplay.Instance.itemCollection.skinArmor++;
                    Gameplay.Instance.itemCollection.UpdateSkinArmor();
                    break;
                case TypeItem.ironArmor:
                    Gameplay.Instance.itemCollection.ironArmor++;
                    Gameplay.Instance.itemCollection.UpdateIronArmor();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GetComponent<BoxCollider>().enabled = false;
            transform.DOMove(other.transform.position+ new Vector3(0,2,0), 0.2f).OnComplete(
                () => gameObject.SetActive(false));
        }
    }
}
