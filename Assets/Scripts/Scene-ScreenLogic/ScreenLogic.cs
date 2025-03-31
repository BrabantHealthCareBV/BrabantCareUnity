using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLogic : MonoBehaviour
{
    // References to the different screens
    public GameObject welcomeScreen;
    public GameObject homeScreen;
    public GameObject loginScreen;
    public GameObject editInfoScreen;
    public GameObject tipsScreen;
    public GameObject registerScreen;


    void Start()
    {
        ShowBeginScreen();
    }

    // Method to show the Begin Screen
    public void ShowBeginScreen()
    {
        SetActiveScreen(welcomeScreen);
    }

    // Method to show the Home Screen
    public void ShowHomeScreen()
    {
        SetActiveScreen(homeScreen);
    }

    // Method to show the Login Screen
    public void ShowLoginScreen()
    {
        SetActiveScreen(loginScreen);
    }

    // Method to show the Edit Info Screen
    public void ShowEditInfoScreen()
    {
        SetActiveScreen(editInfoScreen);
    }

    public void TipsScreen()
    {
        SetActiveScreen(tipsScreen);
    }
    public void RegisterScreen()
    {
        SetActiveScreen(registerScreen);
    }


    // Helper method to set a single active screen and deactivate others
    private void SetActiveScreen(GameObject screenToActivate)
    {
        DeactivateAllScreens();
        if (screenToActivate != null)
        {
            screenToActivate.SetActive(true);
        }
    }

    // Helper method to deactivate all screens
    private void DeactivateAllScreens()
    {
        welcomeScreen.SetActive(false);
        homeScreen.SetActive(false);
        loginScreen.SetActive(false);
        editInfoScreen.SetActive(false);
        registerScreen.SetActive(false);
    }

    // Method to handle the Login Button logic
    public void OnLoginButtonClicked()
    {

        // If there is no data in the DataHandler, go to the Login screen
        if (KeepAlive.Instance.UserToken == "" || KeepAlive.Instance.StoredGuardian == null)
        {
            ShowLoginScreen();
        }
        else
        {
            // If data exists, go to the Edit Info screen
            ShowEditInfoScreen();
        }
    }

    // Method to handle the Back to Home Button logic
    public void OnBackToHomeButtonClicked()
    {
        ShowHomeScreen();
    }
}
