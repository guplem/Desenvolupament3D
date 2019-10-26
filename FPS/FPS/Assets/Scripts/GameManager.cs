using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public PlayerManager player;
private Queue<GameObject> decals = new Queue<GameObject>();

    public static GameManager Instance;
    public bool hasKey { get { return _hasKey; } set { _hasKey = value; keyUi.SetActive(value); } }
    private bool _hasKey = false;
    [SerializeField] private GameObject keyUi;
    [SerializeField] private GameObject deadScreen;
    [SerializeField] private Camera cam;
    
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        } 
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }        
        else if (Input.GetKeyDown(KeyCode.K))
        {
            player.GetComponent<Health>().Hurt(1000);
        }
    }


    public void PlayerDead()
    {
        Destroy(cam.GetComponent<AudioListener>());
        deadScreen.SetActive(true);
    }
}
