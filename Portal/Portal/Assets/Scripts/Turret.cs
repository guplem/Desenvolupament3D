using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private float m_AngleLaserActive=60.0f;
    [SerializeField] private GameObject laser;

    private void Update()
    {
        float l_DotAngleLaserActive=Mathf.Cos(m_AngleLaserActive*Mathf.Deg2Rad*0.5f);
        laser.SetActive(Vector3.Dot(transform.up, Vector3.up)>l_DotAngleLaserActive);
    }
    
    
    
}
