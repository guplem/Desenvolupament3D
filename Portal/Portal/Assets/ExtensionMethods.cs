using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static void ResetTransform(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1, 1);
    }
    
    public static void SetTransform(this Transform trans, Transform transToCopy)
    {
        trans.position = transToCopy.position;
        trans.localRotation = transToCopy.localRotation;
        trans.localScale = transToCopy.localScale;
    }
    
}
