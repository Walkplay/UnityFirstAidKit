using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IconList", menuName = "DataTypes/Icon List")]
public class IconsSO : IContent
{
    [SerializeField] public List<UnitIconSO> icons;

    public override IData GetItem(int index)
    {
        return icons[index];
    }

    public override int GetListCount()
    {
        return icons.Count;
    }
}
