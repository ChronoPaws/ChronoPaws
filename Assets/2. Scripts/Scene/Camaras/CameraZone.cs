using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public Camera cameraToActivate;
    public Camera cameraToDeactivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activar la nueva c�mara
            if (cameraToActivate != null)
                cameraToActivate.gameObject.SetActive(true);

            if (cameraToDeactivate != null)
                cameraToDeactivate.gameObject.SetActive(false);

            // Actualizar el Parallax para seguir la nueva c�mara
            ParallaxMovement parallax = Object.FindFirstObjectByType<ParallaxMovement>();
            if (parallax != null)
            {
                parallax.SetCamera(cameraToActivate.transform);
            }
        }
    }
}

