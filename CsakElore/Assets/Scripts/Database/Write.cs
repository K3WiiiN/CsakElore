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
{   //Belépés
    public TextMeshProUGUI username;
    public TextMeshProUGUI password;


    //Bejelentkezve
    public string loggedInUsername;
    public TextMeshProUGUI loggedInUserText;

    //Regisztráció
    public TextMeshProUGUI regusername;
    public TextMeshProUGUI regemail;
    public TextMeshProUGUI regpassword;
    public TextMeshProUGUI regpasswordaccept;


    //Hibaüzenetek - regisztráció
    public TextMeshProUGUI errorMessageRegMezo;
    public TextMeshProUGUI errorMessageRegFelhnev;
    public TextMeshProUGUI errorMessageRegEmail;
    public TextMeshProUGUI errorMessageRegJelszo;
    public TextMeshProUGUI errorMessageRegJelszoEgyf;


    //Sikeres regisztráció
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

        // Már létezõ felhasználó?
        MySqlCommand checkUserCommand = new MySqlCommand($"SELECT COUNT(*) FROM users WHERE felhasznalonev = '{regusername.text}'", MS_Connection);
        int userCount = Convert.ToInt32(checkUserCommand.ExecuteScalar());

        if (userCount > 0)
        {
            errorMessageRegMezo.gameObject.SetActive(true);
        }
        else
        {
            // Ellenõrizzük, hogy minden regisztrációs mezõ kitöltve van-e
            if (string.IsNullOrWhiteSpace(regusername.text) || string.IsNullOrWhiteSpace(regemail.text) || string.IsNullOrWhiteSpace(regpassword.text) || string.IsNullOrWhiteSpace(regpasswordaccept.text))
            {
                errorMessageRegMezo.gameObject.SetActive(true);
                errorMessageRegFelhnev.gameObject.SetActive(false);
                errorMessageRegEmail.gameObject.SetActive(false);
                errorMessageRegJelszo.gameObject.SetActive(false);
                errorMessageRegJelszoEgyf.gameObject.SetActive(false);
                successMessageReg.gameObject.SetActive(false);
                return; // Kilépünk a metódusból, ha bármelyik mezõ üres
            }
            // Ellenõrizzük, hogy a felhasználónév legalább 4 karakter hosszú-e
            if (regusername.text.Length < 4)
            {
                errorMessageRegFelhnev.gameObject.SetActive(true);
                errorMessageRegMezo.gameObject.SetActive(false);
                errorMessageRegEmail.gameObject.SetActive(false);
                errorMessageRegJelszo.gameObject.SetActive(false);
                errorMessageRegJelszoEgyf.gameObject.SetActive(false);
                successMessageReg.gameObject.SetActive(false);
                return; // Kilépünk a metódusból, ha a felhasználónév kevesebb, mint 4 karakter
            }
            // Ellenõrizzük, hogy az email cím tartalmazza-e az '@' és '.' karaktereket
            if (!regemail.text.Contains("@") || !regemail.text.Contains("."))
            {
                errorMessageRegEmail.gameObject.SetActive(true);
                errorMessageRegMezo.gameObject.SetActive(false);
                errorMessageRegFelhnev.gameObject.SetActive(false);
                errorMessageRegJelszo.gameObject.SetActive(false);
                errorMessageRegJelszoEgyf.gameObject.SetActive(false);
                successMessageReg.gameObject.SetActive(false);
                return; // Kilépünk a metódusból, ha az email cím nem megfelelõ formátumú
            }
            // Ellenõrizzük, hogy a jelszó legalább 8 karakter hosszú-e
            if (regpassword.text.Length < 8)
            {
                errorMessageRegJelszo.gameObject.SetActive(true);
                errorMessageRegMezo.gameObject.SetActive(false);
                errorMessageRegFelhnev.gameObject.SetActive(false);
                errorMessageRegEmail.gameObject.SetActive(false);
                errorMessageRegJelszoEgyf.gameObject.SetActive(false);
                successMessageReg.gameObject.SetActive(false);
                return; // Kilépünk a metódusból, ha a jelszó kevesebb, mint 8 karakter
            }
            // Ellenõrizzük, hogy a két jelszó egyezik-e
            if (regpassword.text != regpasswordaccept.text)
            {
                errorMessageRegJelszoEgyf.gameObject.SetActive(true);
                errorMessageRegMezo.gameObject.SetActive(false);
                errorMessageRegFelhnev.gameObject.SetActive(false);
                errorMessageRegEmail.gameObject.SetActive(false);
                errorMessageRegJelszo.gameObject.SetActive(false);
                successMessageReg.gameObject.SetActive(false);
                return; // Kilépünk a metódusból, ha a két jelszó nem egyezik
            }

            string hashedPassword = HashHelper.CalculateSHA256(regpassword.text);
            //Regisztráció
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