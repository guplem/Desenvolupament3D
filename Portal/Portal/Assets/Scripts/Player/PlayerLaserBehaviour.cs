using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLaserBehaviour : MonoBehaviour, LaserBehaviour
{

    public void OnHitStart(Vector3 woldPosition)
    {
        GameManager.Instance.Kill();
    }

    public void OnHitEnd()
    {
        
    }
}
