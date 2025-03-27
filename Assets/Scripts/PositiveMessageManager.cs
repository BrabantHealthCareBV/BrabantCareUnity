using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PositiveMessageManager : MonoBehaviour
{
    public List<Sprite> PositiveImages;
    public GameObject Popup;
    public UnityEngine.UI.Image PopupImage;

    private int lastIndex = -1; // Store the index of the last displayed image

    public void PopUpScreen()
    {
        if (PositiveImages.Count == 0)
        {
            Debug.LogWarning("Geen afbeeldingen gevonden.");
            return;
        }

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, PositiveImages.Count);
        } while (randomIndex == lastIndex);

        lastIndex = randomIndex; // Update the last index
        Sprite selectedImage = PositiveImages[randomIndex];

        if (PopupImage != null)
        {
            PopupImage.sprite = selectedImage;
        }
        else
        {
            Debug.LogWarning("PopupImage reference is not set.");
        }

        if (Popup != null)
        {
            Popup.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Popup reference is not set.");
        }
    }
}
