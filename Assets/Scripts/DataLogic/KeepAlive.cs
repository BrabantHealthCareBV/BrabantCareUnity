using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepAlive : MonoBehaviour
{
    public static KeepAlive Instance { get; private set; }

    // Persistent Data
    public Doctor StoredDoctor { get; set; } = new();
    public Guardian StoredGuardian { get; set; } = new();
    public Patient StoredPatient { get; set; } = new();
    public string UserToken = "";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject targetObject = GameObject.Find("WebClient");
        if (targetObject != null)
        {
            WebClient webClient = targetObject.GetComponent<WebClient>();
            if (webClient != null)
            {
                webClient.SetToken(UserToken);
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
