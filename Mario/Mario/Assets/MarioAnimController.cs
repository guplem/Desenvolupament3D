using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioAnimController : MonoBehaviour
{

    private Animator animator;
    
    public float horizontalSpeed
    {
        get { return animator.GetFloat("speed"); } 
        set { animator.SetFloat("speed", value/15f ); Debug.Log(value + " --> " + value/17f); } 
    }
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

}
