using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
 
 public static GameManager Intance {get; private set;}

 private int vidas = 3;
 public HUD hud;


[SerializeField] private GameObject Gameover;

private void Awake() {
   if(Intance == null){
      Intance = this;
   }
   else
   {

      Debug.Log("mas de un game manager en la escena.");
     
   }


}


 public void PerderVida(){
    vidas -= 1;
    if(vidas == 0)
    {
      Time.timeScale = 0;
      Gameover.SetActive(true);

    }
    hud.DesactivarVida(vidas);
    
 }

public void Si(){
        Time.timeScale = 1f;
        Gameover.SetActive(false);
   SceneManager.LoadScene(SceneManager.GetActiveScene().name);
 
         
}

public void No(){
   Gameover.SetActive(false);
   Application.Quit();
}
 public bool RecuperarVida(){
   if(vidas == 3){
      return false;
   }

   hud.ActivarVida(vidas);
   vidas += 1;
   return true;
 }


}
