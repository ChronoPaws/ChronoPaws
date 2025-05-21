using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMeta : MonoBehaviour
{
   public string que_escena;

   private void OnTriggerEnter2D(Collider2D collision) {
    if(collision.CompareTag("Personaje"))
    {
      SceneManager.LoadScene(que_escena);
    }
    
   }
}
