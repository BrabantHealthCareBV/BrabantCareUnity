using Unity.VisualScripting;
using UnityEngine;

public class RouteGameType : MonoBehaviour
{
    public string route;
    private string routeChoise = "routeB";
    public GameObject doorclosed;
    public GameObject doorOpen;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (route == "routeA" && routeChoise == "routeA")
        {
            Debug.Log("RouteA");
            if (other.CompareTag("Player"))
            {
                Debug.Log("Door open!");
                doorOpen.SetActive(true);
                doorclosed.SetActive(false);
            }
        }
        else if (route == "routeB" && routeChoise == "routeB")
        {
            Debug.Log("RouteB");
            if (other.CompareTag("Player"))
            {
                Debug.Log("Door open!");
                doorOpen.SetActive(true);
                doorclosed.SetActive(false);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (route == "routeA" && routeChoise == "routeA")
        {
            Debug.Log("RouteA");
            if (other.CompareTag("Player"))
            {
                Debug.Log("Door closed!");
                doorOpen.SetActive(false);
                doorclosed.SetActive(true);
            }
        }
        else if (route == "routeB" && routeChoise == "routeB")
        {
            Debug.Log("RouteB");
            if (other.CompareTag("Player"))
            {
                Debug.Log("Door closed!");
                doorOpen.SetActive(false);
                doorclosed.SetActive(true);
            }
        }
    }
}
