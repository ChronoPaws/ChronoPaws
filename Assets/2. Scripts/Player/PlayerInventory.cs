using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private bool isInventoryOpen = false;

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;

        if (isInventoryOpen)
        {
            Debug.Log("Inventario abierto");
            // Aquí podrías activar la UI del inventario
        }
        else
        {
            Debug.Log("Inventario cerrado");
            // Aquí podrías desactivar la UI del inventario
        }
    }
}
