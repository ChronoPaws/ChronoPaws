using UnityEditor;
using UnityEngine;

public class coleccion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // prevenir doble contacto
            GetComponent<Collider2D>().enabled = false;
            if (CollectionManager.Instance != null)
            {
                CollectionManager.Instance.AddCollectible();
            }

            Destroy(gameObject);
        }
    }
}