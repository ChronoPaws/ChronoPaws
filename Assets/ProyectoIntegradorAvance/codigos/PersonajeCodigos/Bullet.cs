using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D MyRb;
    public float Speed;
    public float tiempoDestroy;
    public float daño;
    void Start()
    {
        MyRb = GetComponent<Rigidbody2D>();

    }
   

    // Update is called once per frame
    void Update()
    {

        MyRb.linearVelocity = transform.right * Speed;
        Destroy(gameObject, tiempoDestroy);
    }
    private void OnTriggerEnter2D(Collider2D collision_Enemigo)
    {
        if (collision_Enemigo.CompareTag("Enemigo"))
        {
            collision_Enemigo.gameObject.GetComponent<IMatable>().hacerDaño();
            Destroy(gameObject);
            
        }
    }
}