using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Funzilla;
using UnityEngine;
using UnityEngine.UI;

public class Ingredient : MonoBehaviour
{
    private DataConfig _dataConfig => Gameplay.Instance.dataConfigSO.DataConfig;
    public int current;
    public int amount;
    [SerializeField] private Text amountTxt;
    [SerializeField] private SpriteRenderer sprite;
    public TypeItem type;
    public enum TypeItem
    {
       stone,
       wood,
       skin,
       iron,
       diamond,
       woodVip,
       stoneVip,
       woodVip2
    }

    private void Start()
    {
        current = amount;
        amountTxt.text = amount + "x";
        var sp = _dataConfig.ingredientData.First(x => x.type == type);
        sprite.sprite = sp.avatar;
    }

    // Update is called once per frame
    // private void Update()
    // {
    //     amountTxt.text = amount + "x";
    // }

    public void UpdateAmount()
    {
        amountTxt.text = amount + "x";
    }
}
