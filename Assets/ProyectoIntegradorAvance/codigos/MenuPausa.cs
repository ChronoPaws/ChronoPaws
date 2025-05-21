using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{

[SerializeField] private GameObject botonPausa;
[SerializeField] private GameObject menuPausa;
  private bool juegoPausado = false;


  public void Pausa(){
    juegoPausado = true;
    Time.timeScale = 0f;
    botonPausa.SetActive(false);
    menuPausa.SetActive(true);
  }

  public void Reanudar(){
    juegoPausado = false;
    Time.timeScale = 1f;
    botonPausa.SetActive(true);
    menuPausa.SetActive(false);
  }

  public void Reiniciar(){
    juegoPausado = false;
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);

  }

  public void Cerrar(){
    Debug.Log("cerrar juego");
    Application.Quit();
  }




}
