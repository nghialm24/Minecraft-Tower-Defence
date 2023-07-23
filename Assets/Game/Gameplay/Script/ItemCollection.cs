using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    [SerializeField] private GameObject stoneUI;
    public int stone;
    [SerializeField] private TextMeshProUGUI stoneTxt;
    //--------------
    [SerializeField] private GameObject woodUI;
    public int wood;
    [SerializeField] private TextMeshProUGUI woodTxt;
    //--------------
    [SerializeField] private GameObject skinUI;
    public int skin;
    [SerializeField] private TextMeshProUGUI skinTxt;
    //--------------
    [SerializeField] private GameObject ironUI;
    public int iron;
    [SerializeField] private TextMeshProUGUI ironTxt;
    //--------------
    [SerializeField] private GameObject diamondUI;
    public int diamond;
    [SerializeField] private TextMeshProUGUI diamondTxt;    
    //--------------
    [SerializeField] private GameObject woodVipUI;
    public int woodVip;
    [SerializeField] private TextMeshProUGUI woodVipTxt;    
    //--------------
    [SerializeField] private GameObject ironVipUI;
    public int ironVip;
    [SerializeField] private TextMeshProUGUI ironVipTxt;    
    //--------------
    [SerializeField] private GameObject skinArmorUI;
    public int skinArmor;
    [SerializeField] private TextMeshProUGUI skinArmorTxt;    
    //--------------
    [SerializeField] private GameObject ironArmorUI;
    public int ironArmor;
    [SerializeField] private TextMeshProUGUI ironArmorTxt;
    private void Start()
    {
        stoneTxt.text = stone.ToString();
        woodTxt.text = wood.ToString();
        skinTxt.text = skin.ToString();
        ironTxt.text = iron.ToString();
        diamondTxt.text = diamond.ToString();
        woodVipTxt.text = woodVip.ToString();
        ironVipTxt.text = ironVip.ToString();
        skinArmorTxt.text = skinArmor.ToString();
        ironArmorTxt.text = ironArmor.ToString();
    }
    
    public void UpdateStone()
    {
        stoneTxt.text = stone.ToString();
    }
    
    public void UpdateWood()
    {
        woodTxt.text = wood.ToString(); 
    }
    
    public void UpdateSkin()
    {
        skinTxt.text = skin.ToString(); 
    }
    
    public void UpdateIron()
    {
        ironTxt.text = iron.ToString(); 
    }
    
    public void UpdateDiamond()
    {
        diamondTxt.text = diamond.ToString(); 
    }    
    
    public void UpdateWoodVip()
    {
        woodVipTxt.text = woodVip.ToString(); 
    }    
    
    public void UpdateIronVip()
    {
        ironVipTxt.text = ironVip.ToString(); 
    }    
    
    public void UpdateSkinArmor()
    {
        skinArmorTxt.text = skinArmor.ToString(); 
    }    
    
    public void UpdateIronArmor()
    {
        ironArmorTxt.text = ironArmor.ToString(); 
    }

    public void AddItem()
    {
        stone += 1000;
        wood += 1000;
        skin += 1000;
        iron += 1000;
        diamond += 1000;
        woodVip += 1000;
        ironVip += 1000;
        skinArmor += 1000;
        ironArmor += 1000;
        UpdateStone();
        UpdateWood();
        UpdateSkin();
        UpdateIron();
        UpdateDiamond();
        UpdateWoodVip();
        UpdateIronVip();
        UpdateSkinArmor();
        UpdateIronArmor();
    }
}
