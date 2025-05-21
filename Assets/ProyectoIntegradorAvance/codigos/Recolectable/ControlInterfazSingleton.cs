using UnityEngine;

public class ControlInterfazSingleton : MonoBehaviour
{
    public static ControlInterfazSingleton instancia;
    void Awake()
    {
        if (ControlInterfazSingleton.instancia == null)
        {
            // instancia encontrada por primera vez
            ControlInterfazSingleton.instancia = this;
            
        }
        else
        {
            // instancia encontrada por segunda vez
            Destroy(gameObject);
        }
    }
}
