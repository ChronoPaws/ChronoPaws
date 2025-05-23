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

          
                // Sumar al contador
        if (CollectionManager.Instance != null)
            {
                CollectionManager.Instance.AddCollectible();
            }

            // Reproducir sonido desde el Player
            PlayerSoundController controller = collision.GetComponent<PlayerSoundController>();
            if (controller != null)
            {
                controller.playMov1();
            }

            // Destruir el objeto coleccionable
            Destroy(gameObject);
        }
    }
}