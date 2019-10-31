﻿using UnityEngine;

public class RotationController : MonoBehaviour
{
    private float m_Yaw;
    private float m_Pitch;
    private float m_PitchRotationalSpeed=180.0f;
    private float m_MinPitch=-80.0f;
    private float m_MaxPitch=50.0f;
    [SerializeField] private Transform m_PitchControllerTransform;
    private bool m_InvertedYaw=false;
    private bool m_InvertedPitch=true;
    

    void Start()
    {
        m_Yaw=transform.rotation.eulerAngles.y;
        m_Pitch=m_PitchControllerTransform.localRotation.eulerAngles.x;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (!Application.isFocused)
            return;
        
        // Pitch
        float l_MouseAxisY=Input.GetAxis("Mouse Y"); // Obtenir el moviment vertical del ratolí
        m_Pitch+=l_MouseAxisY*m_PitchRotationalSpeed*Time.deltaTime*(m_InvertedPitch?-1:1); // Calcular el pitch que s'aurà d'aplicar
        m_Pitch=Mathf.Clamp(m_Pitch, m_MinPitch, m_MaxPitch); // Limitar el pitch dins dels valors desitjats
        m_PitchControllerTransform.localRotation=Quaternion.Euler(m_Pitch, 0.0f, 0.0f); // Aplicar el pitch

        // Yaw
        float l_MouseAxisX=Input.GetAxis("Mouse X"); // Obtenir el moviment horitzontal del ratolí
        m_Yaw+=l_MouseAxisX*m_PitchRotationalSpeed*Time.deltaTime*(m_InvertedYaw?-1:1); // Calcular el yaw que s'aurà d'aplicar
        transform.rotation=Quaternion.Euler(0.0f, m_Yaw, 0.0f);// Aplicar el yaw

    } 
}
