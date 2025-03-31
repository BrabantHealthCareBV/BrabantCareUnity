using UnityEngine;

public class Button2D : MonoBehaviour
{
    public int link;
    private void OnMouseDown()
    {
        switch (link)
        {
            case 1:
                Application.OpenURL("https://www.amphia.nl/");
                break;
                case 2:
                Application.OpenURL("https://www.amphia.nl/patienten-en-bezoekers/kinderen/kinderen-7-12/iets-gebroken/");
                break;
        }
    }
}
