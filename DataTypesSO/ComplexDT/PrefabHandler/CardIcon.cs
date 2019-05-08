using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardIcon : PrefabHandler
{
    UnitIconSO iconData;

    public Text _ManaCost;
    public Image _Image;
    public Text _Name;


    public override void InjectData(IData data)
    {
        iconData = data as UnitIconSO;
    }

    private void Start()
    {
        _ManaCost.text = iconData.CastPrice.ToString();
        _Name.text = iconData.Name;
        _Image.sprite = iconData._sprite ?? default;
    }
}
