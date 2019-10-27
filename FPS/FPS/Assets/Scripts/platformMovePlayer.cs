using UnityEngine;

public class platformMovePlayer : MonoBehaviour
{
    [SerializeField] private platform platform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
            platform.isPlayerOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            platform.isPlayerOn = false;
        }
    }
}
