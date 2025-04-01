using UnityEngine;
using UnityEngine.UI;
public class AvaterPickLogic : MonoBehaviour
{
    public Button Red, Blue, Green;

    public void OnHoverEnter(Button button)
    {
        button.transform.localScale = new Vector3(1.2f, 1.2f);
    }

    public void OnHoverExit(Button button)
    {
        button.transform.localScale = Vector3.one;
    }
}
