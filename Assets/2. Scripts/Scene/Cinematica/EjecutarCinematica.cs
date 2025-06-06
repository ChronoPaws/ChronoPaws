using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class EjecutarCinematica : MonoBehaviour
{
    public PlayableDirector PlayableDirector;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            PlayableDirector.Play();
        }
    }
}
