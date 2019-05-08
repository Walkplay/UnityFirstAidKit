using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Game/Level")]
public class LevelSO : ScriptableObject , IData
{
    [SerializeField] public string Name;
    [SerializeField] public int BestScore;
    [SerializeField] public List<WaveSO> Waves;
    [SerializeField] public List<Vector3> SpawnPoints;
   


}



