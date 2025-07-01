using UnityEngine;

public class PlayerPlatformAttach : MonoBehaviour
{
    private Transform currentPlatform;
    private Vector3 lastPlatformPos;

    void Update()
    {
        if (currentPlatform != null)
        {
            Vector3 platformDelta = currentPlatform.position - lastPlatformPos;
            transform.position += platformDelta;
            lastPlatformPos = currentPlatform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            currentPlatform = collision.transform;
            lastPlatformPos = currentPlatform.position;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == currentPlatform)
        {
            currentPlatform = null;
        }
    }
}
