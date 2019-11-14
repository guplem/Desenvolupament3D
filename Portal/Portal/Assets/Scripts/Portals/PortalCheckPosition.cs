using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PortalPositionHitInfo
{
    public GameObject gameObjectAtPosition { get; private set; }
    //public string hitTag { get { return gameObjectAtPosition.tag; }  private set { ; } }
    public Vector3 hitNormal { get; private set; }
    public Vector3 hitPosition { get; private set; }
    public float distanceFromPointToWall { get; set; }

    public PortalPositionHitInfo(GameObject gameObjectAtPosition, Vector3 raycastHitNormal, Vector3 hitPosition, float distanceFromPointToWall)
    {
        this.gameObjectAtPosition = gameObjectAtPosition;
        this.hitNormal = hitNormal;
        this.hitPosition = hitPosition;
        this.distanceFromPointToWall = distanceFromPointToWall;
    }
    
}

public class PortalCheckPosition : MonoBehaviour
{

    public PortalPositionHitInfo GetPortalPositionInfo(Vector3 rayOriginPos)
    {
        Vector3 originPos = rayOriginPos;
        Ray ray=new Ray(originPos, transform.position-originPos);
        RaycastHit raycastHit;
        
        if(Physics.Raycast(ray, out raycastHit))
            return new PortalPositionHitInfo(raycastHit.collider.gameObject, raycastHit.normal, raycastHit.point, Vector3.Distance(transform.position, raycastHit.point));
        else
            return null;
    }
    
    
    private void OnDrawGizmos()
    {
        /*if (Application.isPlaying)
            Gizmos.color = PointHasProperTag(GetPortalPositionInfo(GameManager.Instance.player.playerCamera.transform.position))
                ? Color.green
                : Color.red;
        else*/
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
