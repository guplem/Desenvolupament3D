using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPlaceHolder : MonoBehaviour
{
    [SerializeField] private string ableToSpawnPortalTag;
    [SerializeField] private List<PortalCheckPosition> portalPositionsToCheck;
    [SerializeField] private Transform raycastCheckOriginPos;
    
    [SerializeField] private GameObject quadRenderer;
    [SerializeField] private SpriteRenderer edge;
    [SerializeField] private Sprite portalBlueEdge;
    [SerializeField] private Sprite portalOrangeEdge;


    public bool CanSpawnPortal()
    {
        List<PortalPositionHitInfo> portalPositionsInfo = new List<PortalPositionHitInfo>();
        foreach (PortalCheckPosition portalPos in portalPositionsToCheck)
        {
            portalPositionsInfo.Add(portalPos.GetPortalPositionInfo(raycastCheckOriginPos.position));
        }

        return CanSpawnAPortal(portalPositionsInfo);
    }
    
    private bool CanSpawnAPortal(List<PortalPositionHitInfo> portalPositions)
    {
        Vector3 hitNormal = Vector3.zero;

        //TODO DISTANCE?
        
        foreach (PortalPositionHitInfo portalPosition in portalPositions)
        {
            if (portalPosition == null)
                return false;
            
            if (hitNormal == Vector3.zero)
                hitNormal = portalPosition.hitNormal;

            if (hitNormal != portalPosition.hitNormal)
                return false;
            if (!PointHasProperTag(portalPosition))
                return false;
        }

        return true;
    }
    
    private bool PointHasProperTag(PortalPositionHitInfo positionHitInfo)
    {
        return positionHitInfo.gameObjectAtPosition.CompareTag(ableToSpawnPortalTag);
    }

    public void SetVisuals(bool show)
    {
        quadRenderer.SetActive(show);
    }

    public void SetVisuals(PortalController.PortalType portalType)
    {
        switch (portalType)
        {
            case PortalController.PortalType.Blue:
                edge.sprite = portalBlueEdge;
                break;
            case PortalController.PortalType.Orange:
                edge.sprite = portalOrangeEdge;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(portalType), portalType, null);
        }
    }
}
