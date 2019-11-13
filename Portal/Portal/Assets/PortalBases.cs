using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBases : MonoBehaviour
{
    public float defaultSize { get; set; }

    private void Awake()
    {
        defaultSize = transform.localScale.x;
    }

    public float ModifySize(float difference)
    {
        Transform trans = transform;
        float newSize = trans.localScale.x + difference;
        newSize = Mathf.Clamp(newSize, defaultSize * 0.5f, defaultSize * 2.0f);
        trans.localScale = Vector3.one*newSize;
        return newSize;
    }
}
