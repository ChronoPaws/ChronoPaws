using UnityEngine;
using Unity.Cinemachine;

public class cameracam : MonoBehaviour
{
    [SerializeField] private CinemachineCamera myVirtualcamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myVirtualcamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        myVirtualcamera.gameObject.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        myVirtualcamera.gameObject.SetActive(false);
    }
}
