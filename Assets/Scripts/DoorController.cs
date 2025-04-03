using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject doorclosed;
    public GameObject doorOpen;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Door open!");
            doorOpen.SetActive(true);
            doorclosed.SetActive(false);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Door closed!");
            doorOpen.SetActive(false);
            doorclosed.SetActive(true);
        }
    }
}
