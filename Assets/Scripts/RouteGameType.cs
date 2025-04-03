using Unity.VisualScripting;
using UnityEngine;

public class RouteGameType : MonoBehaviour
{
    public string route;
    private string routeChoice = "routeB";
    public GameObject doorclosed;
    public GameObject doorOpen;
    public GameObject GreyAreaA;
    public GameObject GreyAreaB;

    public void ChangeRoute()
    {
        Debug.Log("Buttonwerkt");
        if (routeChoice == "routeB")
        {
            Debug.Log("codewerktB");
            routeChoice = "routeA";
            GreyAreaActivation();
        }
        else if (routeChoice == "routeB")
        {
            Debug.Log("codewerktvoorA");
            routeChoice = "routeA";
            GreyAreaActivation();
        }
    }




    public void GreyAreaActivation()
    { 
        if (routeChoice == "routeA")
        {
            GreyAreaA.SetActive(false);
            GreyAreaB.SetActive(true);
        }
        else if (routeChoice == "routeB")
        {
            GreyAreaB.SetActive(false);
            GreyAreaA.SetActive(true);
        }
    }

    public void Start()
    {
        GreyAreaActivation();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (route == "routeA" && routeChoice == "routeA")
        {
            Debug.Log("RouteA");
            if (other.CompareTag("Player"))
            {
                Debug.Log("Door open!");
                doorOpen.SetActive(true);
                doorclosed.SetActive(false);
            }
        }
        else if (route == "routeB" && routeChoice == "routeB")
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
        if (route == "routeA" && routeChoice == "routeA")
        {
            Debug.Log("RouteA");
            if (other.CompareTag("Player"))
            {
                Debug.Log("Door closed!");
                doorOpen.SetActive(false);
                doorclosed.SetActive(true);
            }
        }
        else if (route == "routeB" && routeChoice == "routeB")
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
