using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LaserBehaviour
{

    void OnHitStart(Vector3 woldPosition);
    void OnHitEnd();

}
