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
        set { _coins = value; UIManager.Instance.SetCoinsTo(value); }
    }
    private int _coins;
    
    public int health
    {
        get { return _health; }
        set { _health = value; UIManager.Instance.SetHealthTo(value); if (value <= 0) Kill(); }
    }
    private int _health;

    public int lives
    {
        get { return _lives; }
        set { _lives = value; UIManager.Instance.SetLivesTo(value); if (value <= 0) UIManager.Instance.ShowEndGameScreen(); }
    }
    private int _lives;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        coins = 0;
        health = 4;
        lives = 3;
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