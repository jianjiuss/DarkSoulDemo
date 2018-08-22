using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelper
{
    public static Transform DeepFind(this Transform parent, string name)
    {
        Transform result = null;

        foreach(Transform child in parent)
        {
            if(child.name.Equals(name))
            {
                result = child;
            }
            else
            {
                result = DeepFind(child, name);
                if(result != null)
                {
                    break;
                }
            }
        }

        return result;
    }
}
