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
public class Write : MonoBehaviour
{   //Belépés
    public TextMeshProUGUI username;
    public TextMeshProUGUI password;


    //Bejelentkezve
    public string loggedInUsername;
    public TextMeshProUGUI loggedInUserText;

    //Regisztráció
    private TextMeshProUGUI id;
    private TextMeshProUGUI eredmenyid;
    public TextMeshProUGUI regusername;
    public TextMeshProUGUI regemail;
    public TextMeshProUGUI regpassword;
    public TextMeshProUGUI regpasswordaccept;


    //Hibaüzenetek - regisztráció
    public TextMeshProUGUI errorMessageReg;
    public TextMeshProUGUI successMessageReg;

    //Hibaüzenetek - bejelentkezés
    public TextMeshProUGUI errorMessageLog;



    //Kapcsolódás
    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    string query;


    //Adatbázis kapcsolódás
    public void Connection()
    {
        connectionString = "Server = localhost ; Database = csakelore ; User = root ; Password=  ; Charset = utf8 ;";
        MS_Connection = new MySqlConnection(connectionString);
        MS_Connection.Open();

    }

    public void Registration()
    {
        Connection();

        //Már létezõ felhasználó?
        MySqlCommand checkUserCommand = new MySqlCommand($"SELECT COUNT(*) FROM users WHERE felhasznalonev = '{regusername.text}'", MS_Connection);
        int userCount = Convert.ToInt32(checkUserCommand.ExecuteScalar());

        if (userCount > 0)
        {
            errorMessageReg.gameObject.SetActive(true);
        }
        else
        {
            // Ellenõrizzük, hogy minden regisztrációs mezõ kitöltve van-e
            if (string.IsNullOrWhiteSpace(regusername.text) || string.IsNullOrWhiteSpace(regemail.text) || string.IsNullOrWhiteSpace(regpassword.text) || string.IsNullOrWhiteSpace(regpasswordaccept.text))
            {
                errorMessageReg.gameObject.SetActive(true);
                successMessageReg.gameObject.SetActive(false);
                return; // Kilépünk a metódusból, ha bármelyik mezõ üres
            }

            string hashedPassword = HashHelper.CalculateSHA256(regpassword.text);
            //Regisztráció
            query = $"insert into users(felhasznalonev , email , jelszo) values('{regusername.text}','{regemail.text}','{hashedPassword}');";
            MS_Command = new MySqlCommand(query, MS_Connection);
            MS_Command.ExecuteNonQuery();
            MS_Connection.Close();
            successMessageReg.gameObject.SetActive(true);
            errorMessageReg.gameObject.SetActive(false);
        }
    }




    //Bejelentkezés
    public void Login()
    {
        Connection();

        //Üres mezõk?
        if (string.IsNullOrWhiteSpace(username.text) || string.IsNullOrWhiteSpace(password.text))
        {
            Debug.Log("Üres mezõ");
            errorMessageLog.gameObject.SetActive(true);
            return;
        }
        string hashedPassword = HashHelper.CalculateSHA256(password.text);

        //Már létezõ felhasználó?
        MySqlCommand checkUserCommand = new MySqlCommand($"SELECT COUNT(*) FROM users WHERE felhasznalonev = '{username.text}' AND jelszo = '{hashedPassword}'", MS_Connection);
        int userCount = Convert.ToInt32(checkUserCommand.ExecuteScalar());

        if (userCount > 0)
        {
            // Sikeres bejelentkezés
            loggedInUsername = username.text;
            PlayerPrefs.SetString("LoggedInUsername", loggedInUsername); // Beállítjuk a bejelentkezett felhasználó nevét PlayerPrefs-ben

            LoadFomenu();
            Debug.Log("Sikeres bejelentkezés!");
        }
        else
        {
            // Sikertelen bejelentkezés
            Debug.Log("Sikertelen bejelentkezés!");
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
    //Felhasználó nevének lekérdezése
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


    //Jelszó hashelése (titkosítás)
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