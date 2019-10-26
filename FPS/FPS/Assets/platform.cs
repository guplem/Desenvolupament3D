using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
     [SerializeField] private Transform[] Waypoints;
     [SerializeField] private float speed = 2;
     [SerializeField] private bool moveAlways = true;
     [HideInInspector] public bool isPlayerOn = false;
     
     private int currentPoint = 0;
 
     void Update ()
     {
         if (!moveAlways)
         {
             if (isPlayerOn)
                 currentPoint = Waypoints.Length - 1;
             else
                 currentPoint = 0;
         }
         
         
         if (Vector3.Distance(transform.position, Waypoints[currentPoint].transform.position) > 0.05f)
         {
             transform.position = Vector3.MoveTowards(transform.position, Waypoints[currentPoint].transform.position, speed * Time.deltaTime);
         }
         else
         {
             currentPoint +=1;
         
             if( currentPoint >= Waypoints.Length)
                 currentPoint = 0; 
         }

     }
     
}
