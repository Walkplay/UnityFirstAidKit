using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TargetDetectAbility : MonoBehaviour
{
    //public GameObjectHolder objectHolder;
    public string hateTag;

    private GameObject target;

    

    void Start()
    {
        //target = objectHolder.gameObject;
        ResetTarget();
    }

    public GameObject GetTarget()
    { return target; }

    public void ResetTarget()
    {
        //GameObject[] builds = SelectLayer(hateLayer);
        GameObject[] builds = GameObject.FindGameObjectsWithTag(hateTag);
        Debug.Log($"{builds.Length} targert found!");
        target = GetClosestObject(builds);
    }

   

    static GameObject[] SelectLayer(int layerNum)
    {
        var objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Deep);

        var list = new List<GameObject>(objs.Length);

        foreach (var obj in objs)
        {
            var go = obj as GameObject;

            if (go == null) continue;

            if (go.layer.CompareTo(layerNum) == 1)
            {
                list.Add(go);
            }

            Selection.objects = list.ToArray();
        }
        return list.ToArray();
        //DB.Log(string.Format("Found {0} objects in layer \"{1}\"", list.Count, string.IsNullOrEmpty(layerName) ? layerNum.ToString() : layerName));
    }
    GameObject GetClosestObject(GameObject[] objects)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject o in objects)
        {
            float dist = Vector3.Distance(o.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = o;
                minDist = dist;
            }
        }
        return tMin;
    }
}
    