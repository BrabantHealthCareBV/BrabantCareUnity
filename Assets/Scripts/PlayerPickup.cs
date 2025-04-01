using TMPro;
using UnityEngine;
public class PlayerPickup : MonoBehaviour
{
    public TextMeshProUGUI itemCounterText;
    private int itemCount = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ItemPickup"))
        {
            itemCount++;
            Debug.Log("Item picked up: " + other.name);
            Destroy(other.gameObject); 
            UpdateCounterUI();
        }
    }

    void UpdateCounterUI()
    {
        itemCounterText.text = "Vormen verzameld: " + itemCount + "/80";
    }
}