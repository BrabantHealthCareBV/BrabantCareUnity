using UnityEngine;
using UnityEngine.UI;

public class AvatarPickerLogic : MonoBehaviour
{
    public Button Red, Blue, Green;
    public GameObject PlayerGreen, PlayerBlue, PlayerRed;
    public GameObject AvatarPicker;

    void Start()
    {
        // Zet alle avatars standaard uit
        AvatarPicker.SetActive(true);

        PlayerGreen.SetActive(false);
        PlayerBlue.SetActive(false);
        PlayerRed.SetActive(false);

        // Controleer of er al een avatar is gekozen en laad deze
        LoadSelectedAvatar();
    }

    public void OnHoverEnter(Button button)
    {
        button.transform.localScale = new Vector3(1.2f, 1.2f);
    }

    public void OnHoverExit(Button button)
    {
        button.transform.localScale = Vector3.one;
    }

    public void SelectGreen()
    {
        KeepAlive.Instance.SelectedAvatar = "Green";
        KeepAlive.Instance.StoredSpawnPosition = GetCurrentPlayerPosition(); // Opslaan huidige positie

        SetActiveAvatar("Green");
        AvatarPicker.SetActive(false);
    }

    public void SelectBlue()
    {
        KeepAlive.Instance.SelectedAvatar = "Blue";
        KeepAlive.Instance.StoredSpawnPosition = GetCurrentPlayerPosition();

        SetActiveAvatar("Blue");
        AvatarPicker.SetActive(false);
    }

    public void SelectRed()
    {
        KeepAlive.Instance.SelectedAvatar = "Red";
        KeepAlive.Instance.StoredSpawnPosition = GetCurrentPlayerPosition();

        SetActiveAvatar("Red");
        AvatarPicker.SetActive(false);
    }

    // Hulpmethode om de huidige positie van de actieve speler op te halen
    private Vector3 GetCurrentPlayerPosition()
    {
        if (PlayerGreen.activeSelf) return PlayerGreen.transform.position;
        if (PlayerBlue.activeSelf) return PlayerBlue.transform.position;
        if (PlayerRed.activeSelf) return PlayerRed.transform.position;

        return KeepAlive.Instance.StoredSpawnPosition; // Fallback
    }

    private void SetActiveAvatar(string avatar)
    {
        KeepAlive.Instance.SelectedAvatar = avatar; // Sla keuze op

        // Haal de laatst opgeslagen spawnpositie op
        Vector3 spawnPos = KeepAlive.Instance.StoredSpawnPosition;
        Debug.Log("Speler spawnt op: " + spawnPos);

        // Activeer de juiste avatar en zet op de juiste positie
        PlayerGreen.SetActive(avatar == "Green");
        PlayerBlue.SetActive(avatar == "Blue");
        PlayerRed.SetActive(avatar == "Red");

        if (avatar == "Green") PlayerGreen.transform.position = spawnPos;
        if (avatar == "Blue") PlayerBlue.transform.position = spawnPos;
        if (avatar == "Red") PlayerRed.transform.position = spawnPos;
    }

    private void LoadSelectedAvatar()
    {
        if (KeepAlive.Instance != null && !string.IsNullOrEmpty(KeepAlive.Instance.SelectedAvatar))
        {
            string avatar = KeepAlive.Instance.SelectedAvatar;
            SetActiveAvatar(avatar);
        }
    }
}
