using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Int", menuName = "DataTypes/Int")]
public class IntVariable : ScriptableObject
{
    [SerializeField] public int value;
}
