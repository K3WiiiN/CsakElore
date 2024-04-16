using System.IO;
using System;
using TMPro;
using UnityEngine;

public class EredmenyekRead : MonoBehaviour
{
    //Id� eredm�nyek text
    public TextMeshProUGUI eredmenytextido1;
    public TextMeshProUGUI eredmenytextido2;
    public TextMeshProUGUI eredmenytextido3;
    public TextMeshProUGUI eredmenytextido4;
    public TextMeshProUGUI eredmenytextido5;

    //D�tum eredm�nyek text
    public TextMeshProUGUI eredmenytextdatum1;
    public TextMeshProUGUI eredmenytextdatum2;
    public TextMeshProUGUI eredmenytextdatum3;
    public TextMeshProUGUI eredmenytextdatum4;
    public TextMeshProUGUI eredmenytextdatum5;



    void Start()
    {
        string loggedInUserName = PlayerPrefs.GetString("LoggedInUsername");
        string directoryPath = Application.persistentDataPath;

        for (int i = 1; i <= 5; i++)
        {
            string fileName = $"{loggedInUserName}_Palya{i}_savedData.txt";
            string filePath = Path.Combine(directoryPath, fileName);

            //F�jl l�tez�se
            if (File.Exists(filePath))
            {
                //F�jl beolvas�sa
                string data = File.ReadAllText(filePath);

                //Adatok feldolgoz�sa
                string[] dataArray = data.Split(',');
                string elapsedTimeString = dataArray[0];
                string dateString = dataArray[1];

                //Adatok megfelel� text-hez rendel�se
                switch (i)
                {
                    case 1:
                        eredmenytextido1.text = elapsedTimeString;
                        eredmenytextdatum1.text = dateString;
                        break;
                    case 2:
                        eredmenytextido2.text = elapsedTimeString;
                        eredmenytextdatum2.text = dateString;
                        break;
                    case 3:
                        eredmenytextido3.text = elapsedTimeString;
                        eredmenytextdatum3.text = dateString;
                        break;
                    case 4:
                        eredmenytextido4.text = elapsedTimeString;
                        eredmenytextdatum4.text = dateString;
                        break;
                    case 5:
                        eredmenytextido5.text = elapsedTimeString;
                        eredmenytextdatum5.text = dateString;
                        break;
                }
            }
            else
            {
                Debug.Log($"Nincs el�rhet� eredm�ny a(z) {fileName} f�jlhoz.");
            }
        }
    }

}
