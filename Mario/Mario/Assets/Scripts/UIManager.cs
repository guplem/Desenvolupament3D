using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private Text coinCounter;
    [SerializeField] private Text livesCounter;
    [SerializeField] private Image healthIndicator;
    [SerializeField] private GameObject healthArea;
    [SerializeField] private GameObject deadScreen;
    [SerializeField] private GameObject endGameScreen;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        deadScreen.SetActive(false);
        endGameScreen.SetActive(false);
    }

    public void SetCoinsTo(int value)
    {
        coinCounter.text = value.ToString();
    }
    
    public void SetHealthTo(int value)
    {

        healthArea.SetActive(true);
        Invoke(nameof(HideHpVisuals), 2f);
        
        healthIndicator.fillAmount = value / 8f;        //Max hp is 8;
    }

    private void HideHpVisuals()
    {
        healthArea.SetActive(false);
    }

    public void ShowDeadScreen()
    {
        deadScreen.SetActive(true);
    }
    
    public void ShowEndGameScreen()
    {
        endGameScreen.SetActive(true);
    }

    public void SetLivesTo(int value)
    {
        livesCounter.text = value.ToString();
    }
}
