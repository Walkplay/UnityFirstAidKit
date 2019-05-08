using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelList", menuName = "DataTypes/Level List")]
public class LevelsSO : IContent
{
    [SerializeField] public List<LevelSO> levels;

    

    public override IData GetItem(int index)
    {
        return levels[index];
    }

    public override int GetListCount()
    {
        return levels.Count;
    }
}
