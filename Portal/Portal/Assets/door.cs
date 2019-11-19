using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] private GameObject doorLeft;
    [SerializeField] private GameObject doorRight;

    private bool inAnim;
    private bool open;

    public void OpenDoor()
    {
        this.inAnim = true;
        open = true;
        doorLeft.SetActive(false);
        doorRight.SetActive(false);
        Debug.Log("OPEN DOOR");
    }

    public void CloseDoor()
    {
        this.inAnim = true;
        open = false;
        doorLeft.SetActive(true);
        doorRight.SetActive(true);
        Debug.Log("CLOSE DOOR");
    }

    /*private void Update()
    {
        if (this.inAnim == false) return;

        Vector3 pos = doorLeft.transform.localPosition;
        float xPos = open ? pos.x + Time.deltaTime : pos.x - Time.deltaTime;
        Debug.Log("OPENING? " + open + " xPos: " + xPos);
        if (xPos >= 1 && open)
        {
            Debug.Log("EX pos");
            xPos = 1;
            this.inAnim = false;
        }
        else if (xPos <= 0 && !open)
        {
            Debug.Log("EX neg");
            xPos = 0;
            this.inAnim = false;
        }
        
        doorLeft.transform.localPosition = new Vector3(xPos, pos.x, pos.y);
        doorRight.transform.localPosition = new Vector3(-xPos, pos.x, pos.y);
    }*/
}
