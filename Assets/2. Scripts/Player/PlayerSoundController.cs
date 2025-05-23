using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip sonidoCorrer;
    public AudioClip sonidoCuracion;
    public AudioClip sonidoRecibirDanio; 
    public AudioClip sonidoAtacar; 
    public AudioClip sonidoSaltar; 
    public AudioClip sonidoMuerte; 
    public AudioClip sonidoRecoger; 
    public AudioClip sonidoCaminar;

    [SerializeField] private AudioSource caminarAudioSource;
    [SerializeField] private AudioClip caminarClip;
    void Start()
    {
        caminarAudioSource.pitch = 0.9f; // aumenta la velocidad un 20%
    }
    public void playCorrer()
    {
        if (caminarAudioSource != null && !caminarAudioSource.isPlaying)
        {
            caminarAudioSource.clip = caminarClip;
            caminarAudioSource.loop = true;
            caminarAudioSource.Play();
        }
    }

    public void stopCorrer()
    {
        if (caminarAudioSource != null && caminarAudioSource.isPlaying)
        {
            caminarAudioSource.Stop();
        }
    }
    public void playCuracion()
    {
        audioSource.PlayOneShot(sonidoCuracion);
    }

    public void playRecibirDanio()
    {
        audioSource.PlayOneShot(sonidoRecibirDanio);
    }

    public void playAtacar()
    {
        audioSource.PlayOneShot(sonidoAtacar);
    }

    public void playSaltar()
    {
        audioSource.PlayOneShot(sonidoSaltar);
    }

    public void playMuerte()
    {
        audioSource.PlayOneShot(sonidoMuerte);
    }

    public void playMov1()
    {
        audioSource.PlayOneShot(sonidoRecoger);
    }

    public void playMov2()
    {
        audioSource.PlayOneShot(sonidoCaminar);
    }
}
