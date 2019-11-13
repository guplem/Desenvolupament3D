
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject portalBlue;
    [SerializeField] public GameObject portalOrange;
    
    
    public static GameManager Instance { get; private set; }
    public PlayerManager player;

    
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
}
