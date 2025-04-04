using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenLogic : MonoBehaviour
{
    public GameObject welcomeScreen;
    public GameObject homeScreen;
    public GameObject loginScreen;
    public GameObject editInfoScreen;
    public GameObject tipsScreen;


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

    public void ShowLoginScreen()
    {
        SetActiveScreen(loginScreen);
    }

    public void ShowEditInfoScreen()
    {
        SetActiveScreen(editInfoScreen);
    }

    public void TipsScreen()
    {
        SetActiveScreen(tipsScreen);
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
        loginScreen.SetActive(false);
        editInfoScreen.SetActive(false);
    }

    public void OnLoginButtonClicked()
    {

        if (KeepAlive.Instance.StoredPatient == null || KeepAlive.Instance.StoredGuardian == null)
        {
            ShowLoginScreen();
        }
        else
        {
            ShowEditInfoScreen();
        }
    }

    public void OnBackToHomeButtonClicked()
    {
        ShowHomeScreen();
    }
}
