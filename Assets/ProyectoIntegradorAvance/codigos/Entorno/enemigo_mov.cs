using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo_mov : MonoBehaviour
{
    public float speed;
    public bool derechasino;
    public float contadort;
    public float contadorcambiar;
    // Start is called before the first frame update
    void Start()
    {
        contadort = contadorcambiar;
    }

    // Update is called once per frame
    void Update()
    {
        if (derechasino == true)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            transform.localScale = new Vector3((float)0.7, (float)0.7, (float)0.7);
        }
         if (derechasino == false)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            transform.localScale = new Vector3((float)-0.7, (float)0.7, (float)0.7);
        }
        contadort -= Time.deltaTime;
        if(contadort <= 0)
        {
            contadort = contadorcambiar;
            derechasino = !derechasino;
        }


    }
}
