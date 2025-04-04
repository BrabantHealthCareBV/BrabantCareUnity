using TMPro;
using UnityEngine;
using System.Collections;
public class PlayerPickup : MonoBehaviour
{
    public TextMeshProUGUI itemCounterText;
    private int itemCount = 0;
    public GameObject itemCounter25Procent;
    public GameObject itemCounter50Procent;
    public GameObject itemCounter75Procent;
    public GameObject itemCounter100Procent;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ItemPickup") && itemCount != 48)
        {
            itemCount++;
            Debug.Log("Item picked up: " + other.name);
            Destroy(other.gameObject); 
            UpdateCounterUI();
            if( itemCount == 12)
            {
                StartCoroutine(ShowAndHideCounter(itemCounter25Procent, 10));
            }
            if (itemCount == 24)
            {
                StartCoroutine(ShowAndHideCounter(itemCounter50Procent, 10));
            }
            if (itemCount == 36)
            {
                StartCoroutine(ShowAndHideCounter(itemCounter75Procent, 10));
            }
            if (itemCount == 48)
            {
                StartCoroutine(ShowAndHideCounter(itemCounter100Procent, 10));
            }
        }
    }
    private IEnumerator ShowAndHideCounter(GameObject image, float activeTime)
    {
        image.SetActive(true);
        yield return new WaitForSeconds(activeTime);
        image.SetActive(false);
    }


    public void UpdateCounterUI()
    {
        itemCounterText.text = "Vormen verzameld: " + itemCount + "/48";
    }
}