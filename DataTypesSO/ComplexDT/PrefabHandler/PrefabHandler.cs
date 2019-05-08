using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrefabHandler : MonoBehaviour
{
    public abstract void InjectData(IData data);
}
