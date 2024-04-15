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
{   //Bel�p�s
    public TextMeshProUGUI username;
    public TextMeshProUGUI password;


    //Bejelentkezve
    public string loggedInUsername;
    public TextMeshProUGUI loggedInUserText;

    //Regisztr�ci�
    private TextMeshProUGUI id;
    private TextMeshProUGUI eredmenyid;
    public TextMeshProUGUI regusername;
    public TextMeshProUGUI regemail;
    public TextMeshProUGUI regpassword;
    public TextMeshProUGUI regpasswordaccept;


    //Hiba�zenetek - regisztr�ci�
    public TextMeshProUGUI errorMessageReg;
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

        //M�r l�tez� felhaszn�l�?
        MySqlCommand checkUserCommand = new MySqlCommand($"SELECT COUNT(*) FROM users WHERE felhasznalonev = '{regusername.text}'", MS_Connection);
        int userCount = Convert.ToInt32(checkUserCommand.ExecuteScalar());

        if (userCount > 0)
        {
            errorMessageReg.gameObject.SetActive(true);
        }
        else
        {
            // Ellen�rizz�k, hogy minden regisztr�ci�s mez� kit�ltve van-e
            if (string.IsNullOrWhiteSpace(regusername.text) || string.IsNullOrWhiteSpace(regemail.text) || string.IsNullOrWhiteSpace(regpassword.text) || string.IsNullOrWhiteSpace(regpasswordaccept.text))
            {
                errorMessageReg.gameObject.SetActive(true);
                successMessageReg.gameObject.SetActive(false);
                return; // Kil�p�nk a met�dusb�l, ha b�rmelyik mez� �res
            }

            string hashedPassword = HashHelper.CalculateSHA256(regpassword.text);
            //Regisztr�ci�
            query = $"insert into users(felhasznalonev , email , jelszo) values('{regusername.text}','{regemail.text}','{hashedPassword}');";
            MS_Command = new MySqlCommand(query, MS_Connection);
            MS_Command.ExecuteNonQuery();
            MS_Connection.Close();
            successMessageReg.gameObject.SetActive(true);
            errorMessageReg.gameObject.SetActive(false);
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