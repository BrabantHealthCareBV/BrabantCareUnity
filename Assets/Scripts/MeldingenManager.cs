using System.Collections;
using UnityEngine;

public class MeldingenManager : MonoBehaviour
{
    public GameObject popUp;
    public float tijdTotMelding = 5f; //Seconden

    void Start()
    {
        StartCoroutine(WachtOpMelding());
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
