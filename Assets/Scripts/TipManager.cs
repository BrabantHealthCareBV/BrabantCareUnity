using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TipManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown categoryDropdown;
    [SerializeField] private TMP_Text tipsText;
    [SerializeField] private Image tipImage; // Image component voor de afbeelding

    // Dictionary om categorieën te koppelen aan afbeeldingen
    private Dictionary<string, Sprite> tipImages = new Dictionary<string, Sprite>();

    private void Start()
    {
        // Laad afbeeldingen (zorg dat ze in de Unity Inspector gekoppeld worden)
        tipImages["Beenbreuk"] = Resources.Load<Sprite>("Images/Beenbreuk");
        tipImages["Armbreuk"] = Resources.Load<Sprite>("Images/Armbreuk");
        tipImages["Gips drooghouden"] = Resources.Load<Sprite>("Images/Douchehoes");
        tipImages["Terug naar school"] = Resources.Load<Sprite>("Images/school");
        tipImages["Jeuk"] = Resources.Load<Sprite>("Images/jeuk");
        tipImages["Tintelend of doof gevoel"] = Resources.Load<Sprite>("Images/tintelend_gevoel");
        tipImages["Gebroken gips"] = Resources.Load<Sprite>("Images/gebroken_gips");
        tipImages["Gips knelt"] = Resources.Load<Sprite>("Images/gips_knelt");

        var options = new List<string>
        {
            "Kies hier een categorie",
            "Armbreuk",
            "Beenbreuk",
            "Gips drooghouden",
            "Terug naar school",
            "Jeuk",
            "Tintelend of doof gevoel",
            "Gebroken gips",
            "Gips knelt"
        };

        categoryDropdown.ClearOptions();
        categoryDropdown.AddOptions(options);
        categoryDropdown.value = 0;
        tipsText.text = "";
        tipImage.gameObject.SetActive(false); // Verberg afbeelding in het begin
        categoryDropdown.onValueChanged.AddListener(delegate { UpdateTips(); });
    }

    private void UpdateTips()
    {
        int selectedIndex = categoryDropdown.value;

        if (selectedIndex == 0)
        {
            tipsText.text = "";
            tipImage.gameObject.SetActive(false);
            return;
        }

        string selectedCategory = categoryDropdown.options[selectedIndex].text;
        tipsText.text = GetTips(selectedCategory);

        // Update afbeelding als er een bestaat
        if (tipImages.ContainsKey(selectedCategory) && tipImages[selectedCategory] != null)
        {
            tipImage.sprite = tipImages[selectedCategory];
            tipImage.gameObject.SetActive(true); // Toon afbeelding
        }
        else
        {
            tipImage.gameObject.SetActive(false); // Verberg als er geen afbeelding is
        }
    }

    private string GetTips(string category)
    {
        switch (category)
        {
            case "Beenbreuk":
                return "- Leg je been op een paar kussen, zodat je voet hoger ligt dan je knie, en je knie hoger dan je heup.\n" +
                       "- Leg in de nacht een kussen onder je matras zodat je voeten hoger liggen.";

            case "Armbreuk":
                return "- Draag overdag een mitella en leg je arm in de nacht op een kussen.\n" +
                       "- Voorkom stijfheid door regelmatig je gewrichten te bewegen.";

            case "Gips drooghouden":
                return "- Zorg dat je gips droog blijft, gebruik een douchehoes.\n" +
                       "- Als het nat wordt, probeer het te drogen met een föhn.";

            case "Terug naar school":
                return "- Gebruik een plastic zak bij slecht weer om het gips droog te houden als het buiten nat is.\n" +
                       "- Vermijd drukte tijdens het spelen of zoek een rustig een plekje tijdens de pauze.";

            case "Jeuk":
                return "- Jeuk ontstaat door vocht tussen de huid en het gips.\n" +
                       "- Probeer te föhnen tussen het gips en de huid, maar gebruik geen scherpe voorwerpen.";

            case "Tintelend of doof gevoel":
                return "- Houd je hand/been 30 minuten hoog en beweeg goed. Doe dit ook als je koude vingers of tenen hebt. \n" +
                       "- Als je je vingers of tenen niet meer kunt bewegen, neem dan contact op met de gipskamer.";

            case "Gebroken gips":
                return "- Neem gelijk contact op met de Amphia gipskamer of spoedeisende hulp bij.";

            case "Gips knelt":
                return "- Houd je arm/been 30 minuten hoog en beweeg je vingers/tenen.";

            default:
                return "Geen tips beschikbaar voor deze categorie.";
        }
    }
}
