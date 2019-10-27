using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera camPlayer;
    private bool iscamPlayerNull;

    private void Start()
    {
        camPlayer = GameManager.Instance.player.playerCamera;
        iscamPlayerNull = camPlayer == null;
    }

    private void Update()
    {
        if (iscamPlayerNull)
            Start();
        
        transform.LookAt(transform.position + camPlayer.transform.rotation * Vector3.forward, camPlayer.transform.rotation * Vector3.up);
    }

    
}
