using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IContent : ScriptableObject
{

    public abstract int GetListCount();
    public abstract IData GetItem(int index);
    


}

   
