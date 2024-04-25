using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Image komponenshez szükséges namespace

public class EredmenyekRead : MonoBehaviour
{
    // Idõ eredmények text
    public TextMeshProUGUI[] eredmenytextido;
    // Dátum eredmények text
    public TextMeshProUGUI[] eredmenytextdatum;
    // Kupa eredmények text
    public TextMeshProUGUI[] eredmenytextkupa;
    // Kupa eredmények image
    public Image[] eredmenyimagekupa; // Image komponensek

    // Kupa képek
    public Sprite aranyKupaKep;
    public Sprite ezustKupaKep;
    public Sprite bronzKupaKep;

    void Start()
    {
        string loggedInUserName = PlayerPrefs.GetString("LoggedInUsername");
        string directoryPath = Application.persistentDataPath;

        for (int i = 1; i <= 5; i++)
        {
            string fileName = $"{loggedInUserName}_Palya{i}_savedData.txt";
            string filePath = Path.Combine(directoryPath, fileName);

            // Fájl létezése
            if (File.Exists(filePath))
            {
                // Fájl beolvasása
                string data = File.ReadAllText(filePath);

                // Adatok feldolgozása
                string[] dataArray = data.Split(',');
                string elapsedTimeString = dataArray[0];
                string dateString = dataArray[1];

                // Kupa meghatározása
                string kupa = KupaHatarozas(elapsedTimeString, i);

                // Adatok megfelelõ text-hez rendelése
                eredmenytextido[i - 1].text = elapsedTimeString;
                eredmenytextdatum[i - 1].text = dateString;
                eredmenytextkupa[i - 1].text = kupa;

                // Képek megjelenítése a megfelelõ kupákhoz
                switch (kupa)
                {
                    case "Arany":
                        eredmenyimagekupa[i - 1].sprite = aranyKupaKep;
                        break;
                    case "Ezüst":
                        eredmenyimagekupa[i - 1].sprite = ezustKupaKep;
                        break;
                    case "Bronz":
                        eredmenyimagekupa[i - 1].sprite = bronzKupaKep;
                        break;
                    default:
                        Debug.Log($"Ismeretlen kupa típus: {kupa}");
                        break;
                }
            }
            else
            {
                Debug.Log($"Nincs elérhetõ eredmény a(z) {fileName} fájlhoz.");
            }
        }
    }

    string KupaHatarozas(string ido, int palyaSzam)
    {
        string[] idoElemek = ido.Split(':');
        int perc = int.Parse(idoElemek[0]);
        int masodperc = int.Parse(idoElemek[1]);

        switch (palyaSzam)
        {
            case 1:
                if ((perc == 0 && masodperc <= 45))
                {
                    return "Arany";
                }
                else if ((perc == 0 && masodperc > 45) && (perc == 0 && masodperc < 55))
                {
                    return "Ezüst";
                }
                else if ((masodperc > 55) || (perc >=1))
                {
                    return "Bronz";
                }
                return "-";
            case 2:
                if ((perc == 0 && masodperc <= 55))
                {
                    return "Arany";
                }
                else if ((perc >= 1) || (perc==0 && masodperc > 55))
                {
                    return "Ezüst";
                }
                else if ((perc > 1 && masodperc >= 05))
                {
                    return "Bronz";
                }
                return "-";
            case 3:
                if ((perc == 0 && masodperc <= 59))
                {
                    return "Arany";
                }
                else if ((perc >= 1))
                {
                    return "Ezüst";
                }
                else if (perc >= 1 && masodperc >= 05)
                {
                    return "Bronz";
                }
                return "-";
            case 4:
                if ((perc < 1) || (perc == 1 && masodperc <= 05))
                {
                    return "Arany";
                }
                else if ((perc == 1 && masodperc > 05) && (perc == 1 && masodperc <= 7))   
                {
                    return "Ezüst";
                }
                else if ((perc >1) || (perc == 1 && masodperc > 10))
                {
                    return "Bronz";
                }
                return "-";
            case 5:
                if (perc <= 10)
                {
                    return "Arany";
                }
                else if (perc <= 20)
                {
                    return "Ezüst";
                }
                else if (perc <= 30)
                {
                    return "Bronz";
                }
                return "-";
            default:
                return "Nincs meghatározva kupa";
        }
    }
}
