using UnityEngine;

public class PlayerMap : MonoBehaviour
{
    private bool isMapOpen = false;

    public void ToggleMap()
    {
        isMapOpen = !isMapOpen;

        if (isMapOpen)
        {
            Debug.Log("Mapa abierto");
            // Aquí puedes activar la UI del mapa
        }
        else
        {
            Debug.Log("Mapa cerrado");
            // Aquí puedes desactivar la UI del mapa
        }
    }
}
