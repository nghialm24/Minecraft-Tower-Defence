using System.Collections;
using System.Collections.Generic;
using Funzilla;
using TMPro;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public CollectedItem.TypeItem typeItem;
    public void DropdownSample(int index)
    {
        switch (index)
        {
            case 0: typeItem = CollectedItem.TypeItem.stone;
                break;
            case 1: typeItem = CollectedItem.TypeItem.wood;
                break;
            case 2: typeItem = CollectedItem.TypeItem.skin;
                break;
            case 3: typeItem = CollectedItem.TypeItem.iron;
                break;
            case 4: typeItem = CollectedItem.TypeItem.diamond;
                break;
            case 5: typeItem = CollectedItem.TypeItem.woodVip;
                break;
            case 6: typeItem = CollectedItem.TypeItem.stoneVip;
                break;
            case 7: typeItem = CollectedItem.TypeItem.woodVip2;
                break;
        }
    }
    [SerializeField] private GameObject stoneUI;
    //public GameObject stone;
    // [SerializeField] private TextMeshProUGUI stoneTxt;
    // //--------------
    [SerializeField] private GameObject woodUI;
    // public int wood;
    // [SerializeField] private TextMeshProUGUI woodTxt;
    // //--------------
    [SerializeField] private GameObject skinUI;
    // public int skin;
    // [SerializeField] private TextMeshProUGUI skinTxt;
    // //--------------
    [SerializeField] private GameObject ironUI;
    // public int iron;
    // [SerializeField] private TextMeshProUGUI ironTxt;
    // //--------------
    [SerializeField] private GameObject diamondUI;
    // public int diamond;
    // [SerializeField] private TextMeshProUGUI diamondTxt;    
    // //--------------
    [SerializeField] private GameObject woodVipUI;
    // public int woodVip;
    // [SerializeField] private TextMeshProUGUI woodVipTxt;    
    // //--------------
    [SerializeField] private GameObject ironVipUI;
    // public int ironVip;
    // [SerializeField] private TextMeshProUGUI ironVipTxt;    
    // //--------------
    [SerializeField] private GameObject skinArmorUI;
    // public int skinArmor;
    // [SerializeField] private TextMeshProUGUI skinArmorTxt;    
    // //--------------
    // public int ironArmor;
    // [SerializeField] private TextMeshProUGUI ironArmorTxt;
    // private void Start()
    // {
    //     stoneTxt.text = stone.ToString();
    //     woodTxt.text = wood.ToString();
    //     skinTxt.text = skin.ToString();
    //     ironTxt.text = iron.ToString();
    //     diamondTxt.text = diamond.ToString();
    //     woodVipTxt.text = woodVip.ToString();
    //     ironVipTxt.text = ironVip.ToString();
    //     skinArmorTxt.text = skinArmor.ToString();
    //     ironArmorTxt.text = ironArmor.ToString();
    // }
    //
    // public void UpdateStone()
    // {
    //     stoneTxt.text = stone.ToString();
    // }
    //
    // public void UpdateWood()
    // {
    //     woodTxt.text = wood.ToString(); 
    // }
    //
    // public void UpdateSkin()
    // {
    //     skinTxt.text = skin.ToString(); 
    // }
    //
    // public void UpdateIron()
    // {
    //     ironTxt.text = iron.ToString(); 
    // }
    //
    // public void UpdateDiamond()
    // {
    //     diamondTxt.text = diamond.ToString(); 
    // }    
    //
    // public void UpdateWoodVip()
    // {
    //     woodVipTxt.text = woodVip.ToString(); 
    // }    
    //
    // public void UpdateIronVip()
    // {
    //     ironVipTxt.text = ironVip.ToString(); 
    // }    
    //
    // public void UpdateSkinArmor()
    // {
    //     skinArmorTxt.text = skinArmor.ToString(); 
    // }    
    //
    // public void UpdateIronArmor()
    // {
    //     ironArmorTxt.text = ironArmor.ToString(); 
    // }
    //
    public void AddItem()
    {
        if (typeItem == CollectedItem.TypeItem.stone)
        {
            for (int i = 0; i < 10; i++)
            {
                var w = Instantiate(stoneUI,Gameplay.Instance.transform);
                w.GetComponent<CollectedItem>().Init(CollectedItem.TypeItem.stone);
            }
        }else if (typeItem == CollectedItem.TypeItem.wood)
        {
            for (int i = 0; i < 10; i++)
            {
                var w =Instantiate(woodUI,Gameplay.Instance.transform);
                w.GetComponent<CollectedItem>().Init(CollectedItem.TypeItem.wood);
            }
        }else if (typeItem == CollectedItem.TypeItem.skin)
        {
            for (int i = 0; i < 10; i++)
            {
                var w = Instantiate(skinUI,Gameplay.Instance.transform);
                w.GetComponent<CollectedItem>().Init(CollectedItem.TypeItem.skin);

            }
        }else if (typeItem == CollectedItem.TypeItem.iron)
        {
            for (int i = 0; i < 10; i++)
            {
                var w =Instantiate(ironUI,Gameplay.Instance.transform);
                w.GetComponent<CollectedItem>().Init(CollectedItem.TypeItem.iron);

            }
        }else if (typeItem == CollectedItem.TypeItem.diamond)
        {
            for (int i = 0; i < 10; i++)
            {
                var w = Instantiate(diamondUI,Gameplay.Instance.transform);
                w.GetComponent<CollectedItem>().Init(CollectedItem.TypeItem.diamond);

            }
        }else if (typeItem == CollectedItem.TypeItem.woodVip)
        {
            for (int i = 0; i < 10; i++)
            {
                var w = Instantiate(woodVipUI,Gameplay.Instance.transform);
                w.GetComponent<CollectedItem>().Init(CollectedItem.TypeItem.woodVip);

            }
        }else if (typeItem == CollectedItem.TypeItem.stoneVip)
        {
            for (int i = 0; i < 10; i++)
            {
                var w = Instantiate(ironVipUI,Gameplay.Instance.transform);                
                w.GetComponent<CollectedItem>().Init(CollectedItem.TypeItem.stoneVip);

            }
        }else if (typeItem == CollectedItem.TypeItem.woodVip2)
        {
            for (int i = 0; i < 10; i++)
            {
                var w = Instantiate(skinArmorUI,Gameplay.Instance.transform);
                w.GetComponent<CollectedItem>().Init(CollectedItem.TypeItem.woodVip2);
            }
        }
    }
}
