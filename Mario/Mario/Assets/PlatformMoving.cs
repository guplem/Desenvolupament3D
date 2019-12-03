using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PlatformMoving : MonoBehaviour
{
    [SerializeField] private Transform targetA;
    [SerializeField] private Transform targetB;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Transform currentTarget;
    private float offset = 0.1f;

    private void Start()
    {
        currentTarget = targetB;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }

    private void Update()
    {
        Vector3 direction = Vector3.Normalize(currentTarget.position - transform.position);
        transform.position += direction * moveSpeed;
        
        if (Vector3.Distance(currentTarget.position, transform.position) < offset)
        {
            currentTarget = currentTarget == targetA ? targetB : targetA;

            StartCoroutine(Rotate(180));
        }
            
    }

    private float GetCurrentRotation()
    {
        return UnityEditor.TransformUtils.GetInspectorRotation(gameObject.transform).x;
    }


    private IEnumerator Rotate(float rotationQtty)
    {
        float alreadyRotated = 0;

        while (true)
        {
            yield return new WaitForEndOfFrame();

            transform.Rotate(rotationSpeed, 0, 0);
            alreadyRotated += rotationSpeed;
                
            if (alreadyRotated >= rotationQtty)
                yield break;
        }
    }

}
