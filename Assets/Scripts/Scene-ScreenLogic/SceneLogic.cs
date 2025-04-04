using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
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

    public static Vector3 GetSpawnPosition()
    {
        return KeepAlive.Instance != null ? KeepAlive.Instance.StoredSpawnPosition : Vector3.zero;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadHomeScreen()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadGameAtRontgen()
    {
        SetSpawnPosition(new Vector3(15.8f, 1, 0));
        LoadGameScene();
    }

    public void LoadGameAtRouteA()
    {
        SetSpawnPosition(new Vector3(27.5f, 6, 0));
        LoadGameScene();
    }

    public void LoadGameAtRouteB()
    {
        SetSpawnPosition(new Vector3(27.5f, -3, 0));
        LoadGameScene();
    }

    public void LoadGameAtHuis()
    {
        SetSpawnPosition(new Vector3(64.8f, 1, 0));
        LoadGameScene();
    }
}
