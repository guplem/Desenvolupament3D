using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public int coins
    {
        get { return _coins; }
        set { _coins = value; UIManager.Instance.SetCoinsTo(value);
            if (value >= 3) UIManager.Instance.ShowYouWinScreen();
        }
    }
    private int _coins;
    
    public int health
    {
        get { return _health; }
        set { if (value <= 0 && _health > 0) Kill();  _health = value < 0 ? 0 : value; UIManager.Instance.SetHealthTo(_health);}
    }
    private int _health;

    public int lives
    {
        get { return _lives; }
        set { _lives = value < 0 ? 0 : value; UIManager.Instance.SetLivesTo(_lives); if (_lives <= 0) UIManager.Instance.ShowEndGameScreen(); Debug.Log("LIVES = " + _lives);}
    }
    private int _lives;
    
    private void Awake()
    {
        
        Debug.LogWarning("STARTING AWAKE");
        DontDestroyOnLoad(gameObject);

        if (Instance != null)
        {
            lives = Instance.lives;
            Destroy(Instance.gameObject); 
        }
        else
        {
            lives = 3;
        }

        Instance = this;
        Debug.LogWarning("INSTANCE PM DONE");
    }

    private void Start()
    {
        coins = 0;
        health = 4;
    }
    
    private void Kill()
    {
        lives--;
        UIManager.Instance.ShowDeadScreen();
    }

    public void TakeDamage(int damageAmount)
    {
        health--;
    }
}