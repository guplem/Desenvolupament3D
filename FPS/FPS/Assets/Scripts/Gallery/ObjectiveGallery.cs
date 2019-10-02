using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveGallery : MonoBehaviour
{
    private ShootingGallery sg;
        private int hp = 3;
    
    public void StartMatch(ShootingGallery sg)
    {
        gameObject.SetActive(true);
        this.sg = sg;
        
        //TODO: animations and everything...
    }

    public void Hit()
    {
        hp--;

        if (hp < 0)
        {
            sg.score += 100;
            sg.NextObjective();
            gameObject.SetActive(false);
        }
    }
}
