using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepAlive : MonoBehaviour
{
    public static KeepAlive Instance { get; private set; }
    public Doctor StoredDoctor { get; set; }
    public Guardian StoredGuardian { get; set; }
    public Patient StoredPatient { get; set; }
    public string UserToken { get; set; }

    public Vector3 StoredSpawnPosition { get; set; }
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
}
