using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject spawnedObjectInScene;
    [SerializeField] private Transform spawnPoint;

    public void Spawn()
    {
        spawnedObjectInScene.transform.position = spawnPoint.transform.position;
    }
}
