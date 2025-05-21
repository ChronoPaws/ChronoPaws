using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogo : MonoBehaviour
{

[SerializeField] private GameObject dialogoPanel;
[SerializeField] private TMP_Text dialogoTexto;
public bool isJugadorInRange;

    

private void OnTriggerEnter2D(Collider2D collision) {
    if(collision.gameObject.CompareTag("Personaje")){

    isJugadorInRange =true;
    dialogoPanel.SetActive(true);
    }
}

private void OnTriggerExit2D(Collider2D collision) {
    if(collision.gameObject.CompareTag("Personaje")){
    
    isJugadorInRange = false;
    dialogoPanel.SetActive(false);
    
    }
    
}
}
