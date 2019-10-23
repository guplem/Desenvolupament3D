using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public PlayerManager player;
private Queue<GameObject> decals = new Queue<GameObject>();

    public static GameManager Instance;
    public bool hasKey = false;
    
    public int score
    {
        get { return _score;  }
        set
        {
            _score = value;
        }
    }
    private int _score;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are two game managers", gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddDecal(GameObject newDecal)
    {
        decals.Enqueue(newDecal);

        if (decals.Count > 25)
            Destroy(decals.Dequeue());
    }
}
