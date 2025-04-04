using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepAlive : MonoBehaviour
{
    public static KeepAlive Instance { get; set; }

    // Persistent Data
    public Guardian StoredGuardian = new Guardian();
    public Patient StoredPatient = new Patient();
    public List<Patient> StoredPatients = new List<Patient>();
    public List<Guardian> StoredGuardians = new List<Guardian>();
    public List<TreatmentPlan> TreatmentPlans = new List<TreatmentPlan>();
    public string _userToken = "";
    public string UserToken
    {
        get => _userToken;
        set
        {
            _userToken = value;
            UpdateWebClientToken(); // Call method whenever the token changes
        }
    }

    public Vector3 StoredSpawnPosition { get; set; }

    public string SelectedAvatar { get; set; } = "Green";
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
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene load event
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateWebClientToken(); // Ensure the token is updated when a new scene loads
    }

    private void UpdateWebClientToken()
    {
        GameObject targetObject = GameObject.Find("ApiClient");
        if (targetObject != null)
        {
            WebClient webClient = targetObject.GetComponent<WebClient>();
            if (webClient != null)
            {
                webClient.SetToken(_userToken);
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
