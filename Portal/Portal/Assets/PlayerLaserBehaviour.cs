using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLaserBehaviour : MonoBehaviour, LaserBehaviour
{

    public void OnHitStart(Vector3 woldPosition)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnHitEnd()
    {
        
    }
}
