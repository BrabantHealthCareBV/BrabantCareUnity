using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepAlive : MonoBehaviour
{
    public static KeepAlive Instance { get; set; }

    // Persistent Data
    //public Doctor StoredDoctor { get; set; }
    public Guardian StoredGuardian = new Guardian();
    public Patient StoredPatient = new Patient();
    //public Doctor[] StoredDoctors { get; set; }
    public List<Patient> StoredPatients = new List<Patient>();
    public List<Guardian> StoredGuardians = new List<Guardian>();
    //public Patient SelectedPatient { get; set; } = new();
    //public Guardian SelectedGuardian { get; set; } = new();
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
