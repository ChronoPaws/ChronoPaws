using UnityEngine;

public class UICamera : MonoBehaviour
{
    public Transform player;
    public float xPos;
    public float yPos;
    public float zPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(player.position.x + xPos, player.position.y + zPos, zPos);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + xPos, player.position.y + yPos, zPos);
    }
}
