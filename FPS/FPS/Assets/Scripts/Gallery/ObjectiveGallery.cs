using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveGallery : CustomEvent
{
    [SerializeField] private AnimationClip animationClip;

    [SerializeField] private int pointsEarned;
    
    public override bool DoEvent()
    {
        gameObject.SetActive(true);
        
        GetComponent<Animation>().Play(animationClip.name);
        
        return true;
    }

    public void Dead()
    {
        // TODO: play particles?
        
        ShootingGallery.instance.score += pointsEarned;
        gameObject.SetActive(false);
    }
}