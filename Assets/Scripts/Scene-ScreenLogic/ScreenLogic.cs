using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLogic : MonoBehaviour
{
    public GameObject welcomeScreen;
    public GameObject homeScreen;
    //public GameObject loginScreen;
    //public GameObject editInfoScreen;
    public GameObject tipsScreen;
    public GameObject registerScreen;


    void Start()
    {
        ShowBeginScreen();
    }

    public void ShowBeginScreen()
    {
        SetActiveScreen(welcomeScreen);
    }

    public void ShowHomeScreen()
    {
        SetActiveScreen(homeScreen);
    }

    // Method to show the Login Screen
    //public void ShowLoginScreen()
    //{
    //    SetActiveScreen(loginScreen);
    //}

    // Method to show the Edit Info Screen
    //public void ShowEditInfoScreen()
    //{
    //    SetActiveScreen(editInfoScreen);
    //}

    public void TipsScreen()
    {
        SetActiveScreen(tipsScreen);
    }
    public void RegisterScreen()
    {
        SetActiveScreen(registerScreen);
    }


    private void SetActiveScreen(GameObject screenToActivate)
    {
        DeactivateAllScreens();
        if (screenToActivate != null)
        {
            screenToActivate.SetActive(true);
        }
    }

    private void DeactivateAllScreens()
    {
        welcomeScreen.SetActive(false);
        homeScreen.SetActive(false);
        //loginScreen.SetActive(false);
        //editInfoScreen.SetActive(false);
        registerScreen.SetActive(false);
    }

    public void OnLoginButtonClicked()
    {
        SetActiveScreen(registerScreen);

        //// If there is no data in the DataHandler, go to the Login screen
        //if (KeepAlive.Instance.UserToken == "" || KeepAlive.Instance.StoredGuardian == null)
        //{
        //    ShowLoginScreen();
        //}
        //else
        //{
        //    // If data exists, go to the Edit Info screen
        //    ShowEditInfoScreen();
        //}
    }

    public void OnBackToHomeButtonClicked()
    {
        ShowHomeScreen();
    }
}
