using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Image komponenshez sz�ks�ges namespace

public class EredmenyekRead : MonoBehaviour
{
    // Id� eredm�nyek text
    public TextMeshProUGUI[] eredmenytextido;
    // D�tum eredm�nyek text
    public TextMeshProUGUI[] eredmenytextdatum;
    // Kupa eredm�nyek text
    public TextMeshProUGUI[] eredmenytextkupa;
    // Kupa eredm�nyek image
    public Image[] eredmenyimagekupa; // Image komponensek

    // Kupa k�pek
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

            // F�jl l�tez�se
            if (File.Exists(filePath))
            {
                // F�jl beolvas�sa
                string data = File.ReadAllText(filePath);

                // Adatok feldolgoz�sa
                string[] dataArray = data.Split(',');
                string elapsedTimeString = dataArray[0];
                string dateString = dataArray[1];

                // Kupa meghat�roz�sa
                string kupa = KupaHatarozas(elapsedTimeString, i);

                // Adatok megfelel� text-hez rendel�se
                eredmenytextido[i - 1].text = elapsedTimeString;
                eredmenytextdatum[i - 1].text = dateString;
                eredmenytextkupa[i - 1].text = kupa;

                // K�pek megjelen�t�se a megfelel� kup�khoz
                switch (kupa)
                {
                    case "Arany":
                        eredmenyimagekupa[i - 1].sprite = aranyKupaKep;
                        break;
                    case "Ez�st":
                        eredmenyimagekupa[i - 1].sprite = ezustKupaKep;
                        break;
                    case "Bronz":
                        eredmenyimagekupa[i - 1].sprite = bronzKupaKep;
                        break;
                    default:
                        Debug.Log($"Ismeretlen kupa t�pus: {kupa}");
                        break;
                }
            }
            else
            {
                Debug.Log($"Nincs el�rhet� eredm�ny a(z) {fileName} f�jlhoz.");
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
                    return "Ez�st";
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
                    return "Ez�st";
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
                    return "Ez�st";
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
                    return "Ez�st";
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
                    return "Ez�st";
                }
                else if (perc <= 30)
                {
                    return "Bronz";
                }
                return "-";
            default:
                return "Nincs meghat�rozva kupa";
        }
    }
}
