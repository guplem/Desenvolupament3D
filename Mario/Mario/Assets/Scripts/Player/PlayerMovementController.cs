using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CameraController m_CameraController;
    [SerializeField] private MarioAnimController m_Animator;
    private CharacterController characterController;
    
    [Header("Configuration")]
    [SerializeField] private float walkSpeed;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move(GetHorizontalMovement());
        Move(GetVerticalMovement());
    }

    private Vector3 GetVerticalMovement()
    {
        return Vector3.zero;
        // TODO
    }

    private Vector3 GetHorizontalMovement()
    {
        Vector3 l_Movement=Vector3.zero;
        Vector3 l_Forward=m_CameraController.transform.forward;
        Vector3 l_Right=m_CameraController.transform.right;
        l_Forward.y=0.0f;
        l_Forward.Normalize();
        l_Right.y=0.0f;
        l_Right.Normalize();
        if(Input.GetKey(KeyCode.W))
            l_Movement=l_Forward*walkSpeed;
        else if(Input.GetKey(KeyCode.S))
            l_Movement=-l_Forward*walkSpeed;

        return l_Movement;
    }

    private void Move(Vector3 movement)
    {
        characterController.Move(movement);
    }
}
