using UnityEngine;
using UnityEngine.UI;
public class AvaterPickLogic : MonoBehaviour
{
    public Button Red, Blue, Green;
    public GameObject PlayerGreen, PlayerBlue, PlayerRed;
    public GameObject AvatarPicker;

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
        PlayerGreen.SetActive(true);
        PlayerBlue.SetActive(false);
        PlayerRed.SetActive(false);
        AvatarPicker.SetActive(false);
    }

    public void SelectBlue()
    {
        PlayerBlue.SetActive(true);
        PlayerGreen.SetActive(false);
        PlayerRed.SetActive(false);
        AvatarPicker.SetActive(false);
    }
    public void SelectRed()
    {
        PlayerRed.SetActive(true);
        PlayerGreen.SetActive(false);
        PlayerBlue.SetActive(false);
        AvatarPicker.SetActive(false);
    }
}