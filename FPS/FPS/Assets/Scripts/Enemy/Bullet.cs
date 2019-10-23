using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition == null) return;
        Vector3 direction = targetPosition - transform.position;
        transform.position += direction.normalized * speed;
    }

    [SerializeField] private float speed;

    public void SetTarget(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private Vector3 targetPosition;
}
