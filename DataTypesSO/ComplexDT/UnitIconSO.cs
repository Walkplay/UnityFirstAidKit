using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;


/// <summary>
///     ! Icon represents prefab ! 
/// 
/// </summary>
[CreateAssetMenu(fileName = "New Icon", menuName = "Game/UnitIcon")]
public class UnitIconSO : ScriptableObject, IData
{
    [SerializeField] public string Name;
    [SerializeField] public GameObject Prefab;
    [SerializeField] public Sprite _sprite;
    [SerializeField] public int CastPrice;
    

}
