using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float m_YawRotationalSpeed;
    [SerializeField] private float m_PitchRotationalSpeed;
    [SerializeField] private float m_MinPitch;
    [SerializeField] private float m_MaxPitch;
    [SerializeField] private Transform m_LookAt;
    [SerializeField] private float l_Distance;
    [SerializeField] private LayerMask m_RaycastLayerMask;
    [SerializeField] private float m_OffsetOnCollision;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        Vector3 l_Direction = transform.forward;
        float l_MouseAxisX = 0f;
        float l_MouseAxisY = 0f;
        
        if (Application.isFocused)
        {
            l_MouseAxisX = Input.GetAxis("Mouse X");
            l_MouseAxisY = Input.GetAxis("Mouse Y");
        }
        
        Vector3 l_DesiredPosition = transform.position;

        Vector3 l_EulerAngles = transform.eulerAngles;
        float l_Yaw = (l_EulerAngles.y + 180.0f);
        float l_Pitch = l_EulerAngles.x;

        l_Yaw += m_YawRotationalSpeed * l_MouseAxisX * Time.deltaTime;
        l_Yaw *= Mathf.Deg2Rad;
        if (l_Pitch > 180.0f)
            l_Pitch -= 360.0f;
        l_Pitch += m_PitchRotationalSpeed * (-l_MouseAxisY) * Time.deltaTime;
        l_Pitch = Mathf.Clamp(l_Pitch, m_MinPitch, m_MaxPitch);
        l_Pitch *= Mathf.Deg2Rad;
        l_DesiredPosition = m_LookAt.position + new Vector3(Mathf.Sin(l_Yaw) * Mathf.Cos(l_Pitch) * l_Distance,  Mathf.Sin(l_Pitch) * l_Distance, Mathf.Cos(l_Yaw) * Mathf.Cos(l_Pitch) * l_Distance);
        l_Direction = m_LookAt.position - l_DesiredPosition;


        l_Direction /= l_Distance; //TODO: Isn't it 'normalize'?

        RaycastHit l_RaycastHit;
        Ray l_Ray = new Ray(m_LookAt.position, -l_Direction);
        Debug.DrawRay(l_Ray.origin, l_Ray.direction, Color.magenta, 0.1f );
        if (Physics.Raycast(l_Ray, out l_RaycastHit, l_Distance, m_RaycastLayerMask))
        {
            if (l_RaycastHit.collider.gameObject.layer != LayerMask.NameToLayer("World"))
                Debug.LogWarning("Camera ray hiting with GameObject " + l_RaycastHit.collider.gameObject.name + " with layer " + l_RaycastHit.collider.gameObject.layer, l_RaycastHit.collider.gameObject);

            l_DesiredPosition = l_RaycastHit.point + l_Direction * m_OffsetOnCollision;
        }
            
        transform.forward = l_Direction;
        transform.position = l_DesiredPosition;
    }
}
