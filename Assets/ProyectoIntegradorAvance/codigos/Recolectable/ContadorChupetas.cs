using UnityEngine;
using TMPro;
public class ContadorChupetas : MonoBehaviour, IContable
{
    [SerializeField] private int contador = 0;
    [SerializeField] private int conteo_maximo = 10;
    [SerializeField] private int conteo_Inicial = 0;
    [SerializeField] private AudioSource sonido_amentar;
    [SerializeField] private AudioSource sonido_disminuir;
    [SerializeField] private TextMeshProUGUI texto;
  
    [ContextMenu("Aumentar Contador - Chupetas")]
    public void AmuentarContador()
    {
        contador++;
        if (contador> conteo_maximo) 
        {
            contador = conteo_maximo;
        }   
        texto.text = $"{contador}";
        ReproducirSonidoAumentar();
    }

    [ContextMenu("Disminur Contador - Chupetas")]
    public void DisminuirContador()
    {
        contador--;
        if(contador< 0)
        {
            contador = 0;
        }
        texto.text = $"{contador}";
        ReproducirSonidoDisminuir();// no hay sonido definido
    }

    public void ReproducirAnimacion()
    {
        // la idea es que cuando aumente o disminuya haya animacion
        print("No se ha implementado nada aun");
    }

    public void ReproducirSonidoAumentar()
    {
        sonido_amentar.Play();
    }
    public void ReproducirSonidoDisminuir()
    {
        sonido_disminuir.Play();
    }


    public void Start()
    {
        contador = conteo_Inicial;
        texto.text = $"{contador}";
        //incricipcion a evento -- AumentarContador_chupetas
        Eventos.AumentarContador_chupetas += AmuentarContador;
    }

    public void OnDisable()
    {
        Eventos.AumentarContador_chupetas -= AmuentarContador;
    }
}