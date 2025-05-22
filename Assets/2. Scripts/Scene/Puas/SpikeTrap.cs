using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D collision)
{
if (collision.CompareTag("Player"))
{
PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
if (playerHealth != null && !playerHealth.IsDead())
{
playerHealth.InstantKill(transform); // Mata sin importar inmunidad
}
}
}
}
