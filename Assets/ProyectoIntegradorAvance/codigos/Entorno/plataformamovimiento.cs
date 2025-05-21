using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformamovimiento : MonoBehaviour
{
    public GameObject Objetoamover;
    public Transform Startpoint;
    public Transform Endpoint;
    public float Velocidad;

    private Vector3 MoverHacia;


    private void Start()
    {
        MoverHacia = Endpoint.position;
    }
    private void Update()
    {
        Objetoamover.transform.position = Vector3.MoveTowards(Objetoamover.transform.position, MoverHacia, Velocidad * Time.deltaTime);

        if (Objetoamover.transform.position == Endpoint.position)
        {
            MoverHacia = Startpoint.position;
        }
        if (Objetoamover.transform.position == Startpoint.position)
        {
            MoverHacia = Endpoint.position;
        }


    }
}
