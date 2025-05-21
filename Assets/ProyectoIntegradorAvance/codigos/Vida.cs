using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{

private void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.CompareTag("Personaje")){
        bool vidaRecuperada = GameManager.Intance.RecuperarVida();
        if(vidaRecuperada){
            Destroy(this.gameObject);
        }
        
       
    }

}

}
