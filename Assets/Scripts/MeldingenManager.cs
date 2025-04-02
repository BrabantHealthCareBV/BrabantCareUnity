using System.Collections;
using UnityEngine;

public class MeldingenManager : MonoBehaviour
{
    public GameObject popUp;
    private float tijdTotMelding = 5f; //Seconden
    public GameObject notificationTrigger;

    void Start()
    {
        StartCoroutine(WachtOpMelding());
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            notificationTrigger.SetActive(true);
        }
    }

    IEnumerator WachtOpMelding()
    {
        yield return new WaitForSeconds(tijdTotMelding);
        popUp.SetActive(true);
    }

    public void SluitPopUp()
    {
        popUp.SetActive(false);
        StartCoroutine(WachtOpMelding()); //Loopje
    }
}
