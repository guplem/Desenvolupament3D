using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectiveCubeLaserBehaviour : MonoBehaviour, LaserBehaviour
{
    [SerializeField] private GameObject laser;

    public void OnHitStart(Vector3 woldPosition)
    {
        laser.SetActive(true);
    }

    public void OnHitEnd()
    {
        laser.SetActive(false);
    }
}
