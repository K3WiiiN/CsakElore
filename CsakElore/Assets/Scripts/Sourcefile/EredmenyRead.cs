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
        // F�jl el�r�si �tj�nak meghat�roz�sa
        string fileName = "savedData.txt";
        string directoryPath = Application.persistentDataPath;
        string filePath = Path.Combine(directoryPath, fileName);

        // Ellen�rizz�k, hogy a f�jl l�tezik-e
        if (File.Exists(filePath))
        {
            // F�jl beolvas�sa
            string data = File.ReadAllText(filePath);

            // Az adatok feldolgoz�sa
            string[] dataArray = data.Split(',');
            string elapsedTimeString = dataArray[0];
            string dateString = dataArray[1];

          

            // Itt haszn�lhatod az olvasott adatokat
            Debug.Log("El�rt id�: " + elapsedTimeString);
            Debug.Log("D�tum: " + dateString);

            // TextMeshProUGUI elemek be�ll�t�sa
            eredmenytextido1.text = elapsedTimeString;
            eredmenytextdatum1.text = dateString;
        }
        else
        {
            Debug.LogWarning("A f�jl nem tal�lhat�: " + filePath);
        }
    }
}