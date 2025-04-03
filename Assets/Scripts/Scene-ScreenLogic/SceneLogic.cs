using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    // Statische methode om de spawnpositie in KeepAlive in te stellen
    public static void SetSpawnPosition(Vector3 position)
    {
        if (KeepAlive.Instance != null)
        {
            KeepAlive.Instance.StoredSpawnPosition = position;
            Debug.Log("Spawnpositie opgeslagen: " + position);
        }
        else
        {
            Debug.LogError("❌ KeepAlive.Instance is null! Kan geen spawnpositie opslaan.");
        }
    }

    // Statische methode om de spawnpositie uit KeepAlive op te halen
    public static Vector3 GetSpawnPosition()
    {
        return KeepAlive.Instance != null ? KeepAlive.Instance.StoredSpawnPosition : Vector3.zero;
    }

    // Methode om de GameScene te laden
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Methode om de SampleScene te laden
    public void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Methoden om de speler te spawnen op verschillende posities
    public void LoadGameAtRontgen()
    {
        SetSpawnPosition(new Vector3(15.8f, 1, 0));  // Set de spawnpositie
        LoadGameScene();  // Laad de GameScene
    }

    public void LoadGameAtRouteA()
    {
        SetSpawnPosition(new Vector3(27.5f, 6, 0));  // Set de spawnpositie
        LoadGameScene();  // Laad de GameScene
    }

    public void LoadGameAtRouteB()
    {
        SetSpawnPosition(new Vector3(27.5f, -3, 0));  // Set de spawnpositie
        LoadGameScene();  // Laad de GameScene
    }

    public void LoadGameAtHuis()
    {
        SetSpawnPosition(new Vector3(64.8f, 1, 0));
        LoadGameScene();  // Laad de GameScene
    }
}
