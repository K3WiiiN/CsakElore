using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEditor;
using JetBrains.Annotations;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
public class Write : MonoBehaviour
{   //Bel�p�s
    public TextMeshProUGUI username;
    public TextMeshProUGUI password;


    //Bejelentkezve
    public string loggedInUsername;
    public TextMeshProUGUI loggedInUserText;

    //Regisztr�ci�
    public TextMeshProUGUI regusername;
    public TextMeshProUGUI regemail;
    public TextMeshProUGUI regpassword;
    public TextMeshProUGUI regpasswordaccept;


    //Hiba�zenetek - regisztr�ci�
    public TextMeshProUGUI errorMessageRegMezo;
    public TextMeshProUGUI errorMessageRegFelhnev;
    public TextMeshProUGUI errorMessageRegEmail;
    public TextMeshProUGUI errorMessageRegJelszo;
    public TextMeshProUGUI errorMessageRegJelszoEgyf;


    //Sikeres regisztr�ci�
    public TextMeshProUGUI successMessageReg;

    //Hiba�zenetek - bejelentkez�s
    public TextMeshProUGUI errorMessageLog;



    //Kapcsol�d�s
    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    string query;


    //Adatb�zis kapcsol�d�s
    public void Connection()
    {
        connectionString = "Server = localhost ; Database = csakelore ; User = root ; Password=  ; Charset = utf8 ;";
        MS_Connection = new MySqlConnection(connectionString);
        MS_Connection.Open();

    }

    public void Registration()
    {
        Connection();

        // M�r l�tez� felhaszn�l�?
        MySqlCommand checkUserCommand = new MySqlCommand($"SELECT COUNT(*) FROM users WHERE felhasznalonev = '{regusername.text}'", MS_Connection);
        int userCount = Convert.ToInt32(checkUserCommand.ExecuteScalar());

        if (userCount > 0)
        {
            errorMessageRegMezo.gameObject.SetActive(true);
        }
        else
        {
            // Ellen�rizz�k, hogy minden regisztr�ci�s mez� kit�ltve van-e
            if (string.IsNullOrWhiteSpace(regusername.text) || string.IsNullOrWhiteSpace(regemail.text) || string.IsNullOrWhiteSpace(regpassword.text) || string.IsNullOrWhiteSpace(regpasswordaccept.text))
            {
                errorMessageRegMezo.gameObject.SetActive(true);
                errorMessageRegFelhnev.gameObject.SetActive(false);
                errorMessageRegEmail.gameObject.SetActive(false);
                errorMessageRegJelszo.gameObject.SetActive(false);
                errorMessageRegJelszoEgyf.gameObject.SetActive(false);
                successMessageReg.gameObject.SetActive(false);
                return; // Kil�p�nk a met�dusb�l, ha b�rmelyik mez� �res
            }
            // Ellen�rizz�k, hogy a felhaszn�l�n�v legal�bb 4 karakter hossz�-e
            if (regusername.text.Length < 4)
            {
                errorMessageRegFelhnev.gameObject.SetActive(true);
                errorMessageRegMezo.gameObject.SetActive(false);
                errorMessageRegEmail.gameObject.SetActive(false);
                errorMessageRegJelszo.gameObject.SetActive(false);
                errorMessageRegJelszoEgyf.gameObject.SetActive(false);
                successMessageReg.gameObject.SetActive(false);
                return; // Kil�p�nk a met�dusb�l, ha a felhaszn�l�n�v kevesebb, mint 4 karakter
            }
            // Ellen�rizz�k, hogy az email c�m tartalmazza-e az '@' �s '.' karaktereket
            if (!regemail.text.Contains("@") || !regemail.text.Contains("."))
            {
                errorMessageRegEmail.gameObject.SetActive(true);
                errorMessageRegMezo.gameObject.SetActive(false);
                errorMessageRegFelhnev.gameObject.SetActive(false);
                errorMessageRegJelszo.gameObject.SetActive(false);
                errorMessageRegJelszoEgyf.gameObject.SetActive(false);
                successMessageReg.gameObject.SetActive(false);
                return; // Kil�p�nk a met�dusb�l, ha az email c�m nem megfelel� form�tum�
            }
            // Ellen�rizz�k, hogy a jelsz� legal�bb 8 karakter hossz�-e
            if (regpassword.text.Length < 8)
            {
                errorMessageRegJelszo.gameObject.SetActive(true);
                errorMessageRegMezo.gameObject.SetActive(false);
                errorMessageRegFelhnev.gameObject.SetActive(false);
                errorMessageRegEmail.gameObject.SetActive(false);
                errorMessageRegJelszoEgyf.gameObject.SetActive(false);
                successMessageReg.gameObject.SetActive(false);
                return; // Kil�p�nk a met�dusb�l, ha a jelsz� kevesebb, mint 8 karakter
            }
            // Ellen�rizz�k, hogy a k�t jelsz� egyezik-e
            if (regpassword.text != regpasswordaccept.text)
            {
                errorMessageRegJelszoEgyf.gameObject.SetActive(true);
                errorMessageRegMezo.gameObject.SetActive(false);
                errorMessageRegFelhnev.gameObject.SetActive(false);
                errorMessageRegEmail.gameObject.SetActive(false);
                errorMessageRegJelszo.gameObject.SetActive(false);
                successMessageReg.gameObject.SetActive(false);
                return; // Kil�p�nk a met�dusb�l, ha a k�t jelsz� nem egyezik
            }

            string hashedPassword = HashHelper.CalculateSHA256(regpassword.text);
            //Regisztr�ci�
            query = $"insert into users(felhasznalonev , email , jelszo) values('{regusername.text}','{regemail.text}','{hashedPassword}');";
            MS_Command = new MySqlCommand(query, MS_Connection);
            MS_Command.ExecuteNonQuery();
            MS_Connection.Close();
            successMessageReg.gameObject.SetActive(true);
            errorMessageRegMezo.gameObject.SetActive(false);
            errorMessageRegFelhnev.gameObject.SetActive(false);
            errorMessageRegEmail.gameObject.SetActive(false);
            errorMessageRegJelszo.gameObject.SetActive(false);
            errorMessageRegJelszoEgyf.gameObject.SetActive(false);
        }
    }







    //Bejelentkez�s
    public void Login()
    {
        Connection();

        //�res mez�k?
        if (string.IsNullOrWhiteSpace(username.text) || string.IsNullOrWhiteSpace(password.text))
        {
            Debug.Log("�res mez�");
            errorMessageLog.gameObject.SetActive(true);
            return;
        }
        string hashedPassword = HashHelper.CalculateSHA256(password.text);

        //M�r l�tez� felhaszn�l�?
        MySqlCommand checkUserCommand = new MySqlCommand($"SELECT COUNT(*) FROM users WHERE felhasznalonev = '{username.text}' AND jelszo = '{hashedPassword}'", MS_Connection);
        int userCount = Convert.ToInt32(checkUserCommand.ExecuteScalar());

        if (userCount > 0)
        {
            // Sikeres bejelentkez�s
            loggedInUsername = username.text;
            PlayerPrefs.SetString("LoggedInUsername", loggedInUsername); // Be�ll�tjuk a bejelentkezett felhaszn�l� nev�t PlayerPrefs-ben

            LoadFomenu();
            Debug.Log("Sikeres bejelentkez�s!");
        }
        else
        {
            // Sikertelen bejelentkez�s
            Debug.Log("Sikertelen bejelentkez�s!");
            errorMessageLog.gameObject.SetActive(true);
        }

        MS_Connection.Close();
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("LoggedInUsername"))
        {
            string loggedInUsername = PlayerPrefs.GetString("LoggedInUsername");
            loggedInUserText.text = loggedInUsername;
        }
    }
    //Felhaszn�l� nev�nek lek�rdez�se
    public string GetLoggedInUsername()
    {
        return loggedInUsername;
    }

    public string Fomenu;
    public void LoadFomenu()
    {

        SceneManager.LoadScene(Fomenu);
        loggedInUserText.text = GetLoggedInUsername();

    }


    //Jelsz� hashel�se (titkos�t�s)
    public class HashHelper
    {
        public static string CalculateSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }



}