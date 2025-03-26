using UnityEngine;
using TMPro;

public class TipManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown categoryDropdown;
    [SerializeField] private TMP_Text tipsText;

    private void Start()
    {
        var options = new System.Collections.Generic.List<string>
        {
            "Kies hier een categorie",
            "Beenbreuk",
            "Armbreuk",
            "Gips drooghouden",
            "Jeuk",
            "Koude vingers of tenen",
            "Blauwe/Witte vingers of tenen",
            "Tintelend of doof gevoel",
            "Gebroken gips",
            "Gips knelt"
        };

        categoryDropdown.ClearOptions();
        categoryDropdown.AddOptions(options);
        categoryDropdown.value = 0;
        tipsText.text = "";
        categoryDropdown.onValueChanged.AddListener(delegate { UpdateTips(); });
    }

    private void UpdateTips()
    {
        int selectedIndex = categoryDropdown.value;

        if (selectedIndex == 0)
        {
            tipsText.text = "";
            return;
        }

        string selectedCategory = categoryDropdown.options[selectedIndex].text;
        tipsText.text = GetTips(selectedCategory);
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

            case "Jeuk":
                return "- Jeuk ontstaat door vocht tussen de huid en het gips.\n" +
                       "- Probeer te föhnen tussen het gips en de huid, maar gebruik geen scherpe voorwerpen.";

            case "Koude vingers of tenen":
                return "- Houd je hand/been 30 minuten hoog en beweeg goed.";

            case "Blauwe/Witte vingers of tenen":
                return "- Houd je hand/been 30 minuten hoog.\n" +
                       "- Bij witte vingers/tenen: houd je hand/been juist lager.";

            case "Tintelend of doof gevoel":
                return "- Houd je hand/been 30 minuten hoog en beweeg goed.\n" +
                       "- Als je je vingers of tenen niet meer kunt bewegen, neem dan contact op met de gipskamer.";

            case "Gebroken gips":
                return "- Neem direct contact op met de gipskamer of spoedeisende hulp.";

            case "Gips knelt":
                return "- Houd je arm/been 30 minuten hoog en beweeg je vingers/tenen.";

            default:
                return "Geen tips beschikbaar voor deze categorie.";
        }
    }
}
