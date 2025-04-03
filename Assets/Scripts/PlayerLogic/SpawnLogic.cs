using UnityEngine;

public class SpawnLogic : MonoBehaviour
{
    public GameObject PlayerGreen, PlayerBlue, PlayerRed;

    void Start()
    {
        if (KeepAlive.Instance != null)
        {
            Vector3 spawnPos = KeepAlive.Instance.StoredSpawnPosition;
            string avatar = KeepAlive.Instance.SelectedAvatar;

            Debug.Log("Gekozen avatar: " + avatar);
            Debug.Log("Speler spawnt op: " + spawnPos);

            PlayerGreen.SetActive(false);
            PlayerBlue.SetActive(false);
            PlayerRed.SetActive(false);

            if (avatar == "Green")
            {
                PlayerGreen.SetActive(true);
                PlayerGreen.transform.position = spawnPos;
            }
            else if (avatar == "Blue")
            {
                PlayerBlue.SetActive(true);
                PlayerBlue.transform.position = spawnPos;
            }
            else if (avatar == "Red")
            {
                PlayerRed.SetActive(true);
                PlayerRed.transform.position = spawnPos;
            }
            else
            {
                Debug.LogError("⚠️ Geen geldige avatar gevonden, standaard Green gebruiken.");
                PlayerGreen.SetActive(true);
                PlayerGreen.transform.position = spawnPos;
            }
        }
        else
        {
            Debug.LogError("❌ KeepAlive.Instance is null!");
        }
    }
}