using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PortalPosition
{
    public GameObject gameObjectAtPosition { get; private set; }
    public string hitTag { get { return gameObjectAtPosition.tag; }  private set { ; } }
    public Vector3 hitNormal { get; private set; }
    public Vector3 hitPosition { get; private set; }

    public PortalPosition(GameObject gameObjectAtPosition, Vector3 raycastHitNormal, Vector3 hitPosition)
    {
        this.gameObjectAtPosition = gameObjectAtPosition;
        this.hitNormal = hitNormal;
        this.hitPosition = hitPosition;
    }
}

public class PortalCheckPosition : MonoBehaviour
{

    public PortalPosition GetPortalPosition(Camera playerCamera)
    {
        Vector3 position = playerCamera.transform.position;
        Ray ray=new Ray(position, transform.position-position);
        RaycastHit raycastHit;
        
        if(Physics.Raycast(ray, out raycastHit))
            return new PortalPosition(raycastHit.collider.gameObject, raycastHit.normal, raycastHit.point);
        else
            return null;
    }
    
    public static bool CanSpawnAPortal(List<PortalPosition> portalPositions)
    {
        Vector3 hitNormal = Vector3.zero;
        string hitTag = "";
        
        //TODO DISTANCE?
        
        foreach (PortalPosition portalPosition in portalPositions)
        {
            if (hitNormal == Vector3.zero)
                hitNormal = portalPosition.hitNormal;
            if (string.IsNullOrEmpty(hitTag))
                hitTag = portalPosition.hitTag;

            
            if (hitNormal != portalPosition.hitNormal)
                return false;
            if (string.Compare(hitTag, portalPosition.hitTag, StringComparison.Ordinal) != 0)
                return false;
        }

        return true;
    }

    private bool PointHasProperTag(PortalPosition position)
    {
        
        //TODO Check hitTag == at the spawnable zone tag
        return true;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            Gizmos.color = PointHasProperTag(GetPortalPosition(GameManager.Instance.player.playerCamera))
                ? Color.green
                : Color.red;
        else
            Gizmos.color = Color.white;
        
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
