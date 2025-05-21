using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class playerlugar : MonoBehaviour
{
    public VideoPlayer video;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        video = GetComponent<VideoPlayer>();
        if (other.gameObject.CompareTag("Personaje"))
        {
            print("si esta el trigger");
            video.Play();
            video.loopPointReached += CheckOver;
        }
    }
    void CheckOver(VideoPlayer vp)
    {
        Destroy(gameObject);
    }
}
