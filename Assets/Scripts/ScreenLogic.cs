using UnityEngine;

public class ScreenLogic : MonoBehaviour
{
    // References to the different screens
    public GameObject beginScreen;
    public GameObject homeScreen;
    public GameObject loginScreen;
    public GameObject editInfoScreen;
    public GameObject finalGameScreen;

    private DataHandlerLogic dataHandler;

    void Start()
    {
        // You can get a reference to the DataHandlerLogic from the scene if it's attached to a GameObject
        dataHandler = Object.FindFirstObjectByType<DataHandlerLogic>();

        // Initially, show the begin screen
        ShowBeginScreen();
    }

    // Method to show the Begin Screen
    public void ShowBeginScreen()
    {
        SetActiveScreen(beginScreen);
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

    // Method to show the Final Game Screen (for future use)
    public void ShowFinalGameScreen()
    {
        SetActiveScreen(finalGameScreen);
    }

    // Helper method to set the active screen and deactivate all others
    private void SetActiveScreen(GameObject screenToActivate)
    {
        // Deactivate all screens
        beginScreen.SetActive(false);
        homeScreen.SetActive(false);
        loginScreen.SetActive(false);
        editInfoScreen.SetActive(false);
        finalGameScreen.SetActive(false);

        // Activate the selected screen
        if (screenToActivate != null)
        {
            screenToActivate.SetActive(true);
        }
    }

    // Method to handle the Login Button logic
    public void OnLoginButtonClicked()
    {
        if (dataHandler == null)
        {
            Debug.LogError("DataHandlerLogic reference is missing.");
            return;
        }

        // If there is no data in the DataHandler, go to the Login screen
        if (dataHandler.patient == null || dataHandler.guardian == null)
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
