using UnityEngine;

public class InteractionLogic : MonoBehaviour
{
    public GameObject interactiveMenu;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player walked into the object!");
            interactiveMenu.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player walked away from the object!");
            interactiveMenu.SetActive(false);
        }
    }
}
