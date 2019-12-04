using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class KillByImpact : MonoBehaviour
{
    [SerializeField] private GameObject objectToDestroy;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(objectToDestroy);
    }
}
