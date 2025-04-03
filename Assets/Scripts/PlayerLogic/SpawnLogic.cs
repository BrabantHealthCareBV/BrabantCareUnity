using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnLogic : MonoBehaviour
{
    public Transform player;  // Sleep hier de speler in via de Inspector

    void Start()
    {
        // Controleer of KeepAlive instance bestaat en haal de spawnpositie op
        if (KeepAlive.Instance != null)
        {
            Vector3 spawnPos = KeepAlive.Instance.StoredSpawnPosition;
            Debug.Log("Spawnpositie opgehaald: " + spawnPos);

            // Controleer of de speler een parent heeft
            if (player.parent != null)
            {
                Debug.Log("Speler heeft een parent: " + player.parent.name);
                // Gebruik de wereldpositie in plaats van localPosition
                player.position = spawnPos;  // Plaats de speler op de wereldpositie
            }
            else
            {
                player.position = spawnPos;  // Plaats de speler in de wereldpositie
            }

            Debug.Log("Speler final position: " + player.position);  // Log de uiteindelijke positie
        }
        else
        {
            Debug.LogError("❌ KeepAlive is niet geïnitialiseerd.");
        }
    }

    // Methode om de speler bij de Rontgen te spawnen
    public void SpawnAtRontgen()
    {
        player.position = new Vector3(0, 0, 0);  // Set de positie van de speler
    }

    // Methode om de speler bij Route A te spawnen
    public void SpawnAtRouteA()
    {
        player.position = new Vector3(-13, 0, 0);  // Set de positie van de speler
    }

    // Methode om de speler bij Route B te spawnen
    public void SpawnAtRouteB()
    {
        player.position = new Vector3(-13, 8, 0);  // Set de positie van de speler
    }

    // Methode om de speler bij Huis te spawnen
    public void SpawnAtHuis()
    {
        player.position = new Vector3(13, 4, 0);  // Set de positie van de speler
    }
}
