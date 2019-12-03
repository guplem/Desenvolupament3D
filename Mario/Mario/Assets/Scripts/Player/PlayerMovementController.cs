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
    private CharacterController m_CharacterController;
    
    [Header("Configuration")]
    [SerializeField] private float m_WalkSpeed;
    [SerializeField] private float m_SprintSpeed;
    [SerializeField] private float m_JumpSpeed;
    [SerializeField] private float m_BridgeForce;
    
    
    private bool m_OnGround = false;
    private float m_VerticalSpeed = 0f;
    
    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get Horizontal and Vertical Input
        float horizontalInput = Input.GetAxis ("Horizontal");
        float verticalInput = Input.GetAxis ("Vertical");

        // Calculate the Direction to Move based on the tranform of the Player
        Vector3 moveDirectionForward = m_CameraController.transform.forward * verticalInput;
        Vector3 moveDirectionSide = m_CameraController.transform.right * horizontalInput;
        Vector3 direction = (moveDirectionForward + moveDirectionSide).normalized;
    
        // Calculate walking the distance
        Vector3 displacement = direction * m_WalkSpeed * Time.deltaTime;

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
            displacement=displacement.normalized * m_SprintSpeed * Time.deltaTime;

        //Jump
        if(m_OnGround && Input.GetButtonDown("Jump"))
            m_VerticalSpeed=m_JumpSpeed;
    
        // Apply gravity
        m_VerticalSpeed+=Physics.gravity.y*Time.deltaTime;
        displacement.y=m_VerticalSpeed*Time.deltaTime;
    
        // Apply Movement to Player
        CollisionFlags l_CollisionFlags=m_CharacterController.Move(displacement);
        
        //Apply rotation to Player
        Vector3 displacementDir = displacement.normalized;
        if (horizontalInput != 0 || verticalInput != 0)
            transform.rotation = Quaternion.LookRotation(displacementDir);
            
    
        // Process vertical collisions
        if((l_CollisionFlags & CollisionFlags.Below)!=0)
        {
            m_OnGround=true;
            m_VerticalSpeed=0.0f;
        }
        else
            m_OnGround=false;
        if((l_CollisionFlags & CollisionFlags.Above)!=0 &&  m_VerticalSpeed>0.0f)
            m_VerticalSpeed=0.0f;
    }
    
    
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.tag=="Bridge")
        {
            Rigidbody l_Bridge=hit.collider.attachedRigidbody;
            l_Bridge.AddForceAtPosition(-hit.normal*m_BridgeForce, hit.point);
        }
    }
}
