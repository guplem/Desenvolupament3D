using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioAnimController : MonoBehaviour
{

    public Animator animator { get; private set; }

    public float horizontalSpeed
    {
        get { return animator.GetFloat("speed"); } 
        set { animator.SetFloat("speed", value*3 ); } 
    }
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Step()
    {
        Debug.Log("Step");
    }

    /*public void SetSpeed(float f)
    {
        horizontalSpeed = f;
    }*/
}
