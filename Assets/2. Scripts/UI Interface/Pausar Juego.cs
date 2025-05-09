using UnityEngine;
using UnityEngine.SceneManagement;

public class PausarJuego : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject menuConfirmarSalida;
    public bool juegoPausado = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1; // Restaurar el tiempo antes de cambiar de escena
        SceneManager.LoadScene("1. Menu");
    }

    public void ConfirmarSalida()
    {
        menuConfirmarSalida.SetActive(true); // Muestra el panel de confirmación

    }

    public void CancelarSalida()
    {
        menuConfirmarSalida.SetActive(false); // Oculta el panel de confirmación
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliste del juego");
        Application.Quit(); // Esto solo funciona fuera del editor
    }
}
