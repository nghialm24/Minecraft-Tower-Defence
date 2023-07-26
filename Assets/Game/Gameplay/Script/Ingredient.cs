using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Funzilla;
using TMPro;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    private DataConfig _dataConfig => Gameplay.Instance.dataConfigSO.DataConfig;
    public int current;
    public int amount;
    [SerializeField] private TextMeshPro amountTxt;
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
       ironVip,
       skinArmor,
       ironArmor
    }

    private void Start()
    {
        amountTxt.text = amount + "x";
        var sp = _dataConfig.ingredientData.First(x => x.type == type);
        sprite.sprite = sp.avatar;
    }

    // Update is called once per frame
    private void Update()
    {
        amountTxt.text = amount + "x";
    }
}
