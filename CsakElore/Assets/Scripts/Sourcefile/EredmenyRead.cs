using System.IO;
using System;
using TMPro;
using UnityEngine;

public class EredmenyekRead : MonoBehaviour
{
    public TextMeshProUGUI eredmenytextido1;
    public TextMeshProUGUI eredmenytextido2;
    public TextMeshProUGUI eredmenytextido3;
    public TextMeshProUGUI eredmenytextido4;
    public TextMeshProUGUI eredmenytextido5;

    public TextMeshProUGUI eredmenytextdatum1;
    public TextMeshProUGUI eredmenytextdatum2;
    public TextMeshProUGUI eredmenytextdatum3;
    public TextMeshProUGUI eredmenytextdatum4;
    public TextMeshProUGUI eredmenytextdatum5;


    void Start()
    {
        // Fájl elérési útjának meghatározása
        string fileName = "savedData.txt";
        string directoryPath = Application.persistentDataPath;
        string filePath = Path.Combine(directoryPath, fileName);

        // Ellenõrizzük, hogy a fájl létezik-e
        if (File.Exists(filePath))
        {
            // Fájl beolvasása
            string data = File.ReadAllText(filePath);

            // Az adatok feldolgozása
            string[] dataArray = data.Split(',');
            string elapsedTimeString = dataArray[0];
            string dateString = dataArray[1];

          

            // Itt használhatod az olvasott adatokat
            Debug.Log("Elért idõ: " + elapsedTimeString);
            Debug.Log("Dátum: " + dateString);

            // TextMeshProUGUI elemek beállítása
            eredmenytextido1.text = elapsedTimeString;
            eredmenytextdatum1.text = dateString;
        }
        else
        {
            Debug.LogWarning("A fájl nem található: " + filePath);
        }
    }
}