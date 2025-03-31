using UnityEngine;

public class Button2D : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("2D Button clicked!");
        Application.OpenURL("https://www.amphia.nl/");
    }
}
