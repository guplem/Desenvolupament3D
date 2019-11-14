
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject portalBlue;
    [SerializeField] public GameObject portalOrange;

    [SerializeField] private GameObject deadScreen;
    
    public static GameManager Instance { get; private set; }
    public PlayerManager player;
    
    [SerializeField] private Image crossare;
    [SerializeField] private Sprite fullCrossare;
    [SerializeField] private Sprite blueCorossare;
    [SerializeField] private Sprite orangeCrossare;
    [SerializeField] private Sprite emptyCrossare;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameObject go = gameObject;
        if (portalBlue == null)
            Debug.LogError("portalBlue is null in the " + this, go);
        if (portalBlue == null)
            Debug.LogError("portalOrange is null in the " + this, go);
        
        deadScreen.SetActive(false);
    }
    
    public void PlacePortal(PortalController.PortalType portalType, Transform newTransform)
    {
        switch (portalType)
        {
            case PortalController.PortalType.Blue:
                portalBlue.SetActive(true);
                portalBlue.transform.SetTransform(newTransform);
                break;
            
            case PortalController.PortalType.Orange:
                portalOrange.SetActive(true);
                portalOrange.transform.SetTransform(newTransform);
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(portalType), portalType, null);
        }
    }

    public void Kill()
    {
        deadScreen.SetActive(true);
        player.enabled = false;
        player.GetComponent<PlayerMovementController>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (Input.GetKeyDown(KeyCode.Q))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.R))
        {
            portalBlue.SetActive(false);
            portalOrange.SetActive(false);
        }




        SetCrossare();
    }

    private void SetCrossare()
    {
        if (portalBlue.activeSelf && portalOrange.activeSelf)
            crossare.sprite = fullCrossare;
        
        else if (!portalBlue.activeSelf && portalOrange.activeSelf)
            crossare.sprite = orangeCrossare;
        
        else if (portalBlue.activeSelf && !portalOrange.activeSelf)
            crossare.sprite = blueCorossare;
        
        else if (!portalBlue.activeSelf && !portalOrange.activeSelf)
            crossare.sprite = emptyCrossare;
    }
}
