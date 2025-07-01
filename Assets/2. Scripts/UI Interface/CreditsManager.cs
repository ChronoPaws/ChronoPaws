using UnityEngine;
using UnityEngine.Video;

public class CreditManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject creditsPanel;     // El panel que muestra el video
    public GameObject menuInicial;      // El panel del menú inicial (con los botones)

    [Header("Video")]
    public VideoPlayer videoPlayer;     // VideoPlayer que reproduce el video

    [Header("Audio")]
    public AudioSource menuMusic;       // Música de fondo del menú

    [Header("Settings")]
    public float holdEscapeDuration = 2f; // Segundos que hay que mantener ESC

    private float escHoldTime = 0f;
    private bool isPlayingCredits = false;

    void Start()
    {
        // Cuando el video termine, vuelve automáticamente al menú
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if (isPlayingCredits)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                escHoldTime += Time.deltaTime;
                if (escHoldTime >= holdEscapeDuration)
                {
                    StopCredits();
                }
            }
            else
            {
                escHoldTime = 0f; // Reinicia si deja de mantener ESC
            }
        }
    }

    public void ShowCredits()
    {
        creditsPanel.SetActive(true);   // Muestra el panel del vídeo
        menuInicial.SetActive(false);   // Oculta el menú
        menuMusic.Pause();              // Pausa la música del menú
        videoPlayer.Play();             // Reproduce el vídeo
        isPlayingCredits = true;
        escHoldTime = 0f;
    }

    private void StopCredits()
    {
        videoPlayer.Stop();             // Detiene el vídeo
        creditsPanel.SetActive(false);  // Oculta el panel del vídeo
        menuInicial.SetActive(true);    // Vuelve a mostrar el menú
        menuMusic.Play();               // Reanuda la música del menú
        isPlayingCredits = false;
        escHoldTime = 0f;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        StopCredits();
    }
}
