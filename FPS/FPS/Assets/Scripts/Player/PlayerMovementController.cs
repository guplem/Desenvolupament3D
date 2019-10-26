using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    private CharacterController m_CharacterController;
    private float m_Speed=16.0f;
    private float m_FastSpeedMultiplier = 1.65f;
    private float m_JumpSpeed = 5f;
    private float m_VerticalSpeed = 0f;
    private bool m_OnGround;
    
    void Awake()
    {
        m_CharacterController=GetComponent<CharacterController>();
    }
    
    void Update()
    {
        // Get Horizontal and Vertical Input
        float horizontalInput = Input.GetAxis ("Horizontal");
        float verticalInput = Input.GetAxis ("Vertical");

        // Calculate the Direction to Move based on the tranform of the Player
        Vector3 moveDirectionForward = transform.forward * verticalInput;
        Vector3 moveDirectionSide = transform.right * horizontalInput;
        Vector3 direction = (moveDirectionForward + moveDirectionSide).normalized;
        
        // Calculate walking the distance
        Vector3 distance = direction * m_Speed * Time.deltaTime;

        //Sprint
        float l_SpeedMultiplier=1.0f;
        if(Input.GetKey(KeyCode.LeftShift))
            l_SpeedMultiplier=m_FastSpeedMultiplier;
        distance*=Time.deltaTime*m_Speed*l_SpeedMultiplier;

        //Jump
        if(m_OnGround && Input.GetButtonDown("Jump"))
            m_VerticalSpeed=m_JumpSpeed;
        
        // Apply gravity
        m_VerticalSpeed+=Physics.gravity.y*Time.deltaTime;
        distance.y=m_VerticalSpeed*Time.deltaTime;
        
        // Apply Movement to Player
        CollisionFlags l_CollisionFlags=m_CharacterController.Move(distance);
        
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

}
