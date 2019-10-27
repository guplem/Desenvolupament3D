using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixRotation : MonoBehaviour
{
    [SerializeField] private float rotationSearchVelocity = 10f;

    // Update is called once per frame
    void Update()
    {
        Vector3 eulerAngles = transform.eulerAngles;
        float newRotation = eulerAngles.y + rotationSearchVelocity * Time.deltaTime;
        transform.eulerAngles = new Vector3(eulerAngles.x, newRotation, eulerAngles.z);
    }
}
