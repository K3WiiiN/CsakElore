using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    //BELÉPÉS MENÜ-------------------
    //Bejelentkezés menü betöltése (belépés gomb)
    public string Bejelentkezes;

    public void LoadBejelentkezes()
    {

        SceneManager.LoadScene(Bejelentkezes);
    }

    public void ExitGame()
    {
        // A program bezárása (kilépés gomb)
        Application.Quit();
    }
    //BELÉPÉS MENÜ VÉGE-------------------


    //BEJELENTKEZÉS MENÜ-------------------
    //Fõmenü megnyitása (bejelentkezés gomb)
    //Write.cs Login() metódus betölti a 'Fomenu' scene-t sikeres bejelentkezés során


    //Kilépés BELEPES menübe (kilepes gomb)
    public string Belepes;

    public void LoadBelepes()
    {

        SceneManager.LoadScene(Belepes);
    }
    //BEJELENTKEZÉS MENÜ VÉGE-------------------



    //FÕMENÜ MENÜ-------------------
    //Pályaválasztó betöltése
    public string Palyavalaszto;

    public void LoadPalyavalaszto()
    {

        SceneManager.LoadScene(Palyavalaszto);
    }


    //Beállítások betöltése
    public string Beallitasok;

    public void LoadBeallitasok()
    {

        SceneManager.LoadScene(Beallitasok);
    }

    //Eredmenyek menu


   
    //FÕMENÜ MENÜ VÉGE-------------------



    //PÁLYAVÁLASZTÓ MENÜ-------------------
    //Tutorial világ betöltése
    public string Tutorialvilag;

    public void LoadTutorialvilag()
    {

        SceneManager.LoadScene(Tutorialvilag);
    }

    //Kilépés - Fõmenübe (kilépés gombra)
    public string Fomenu;

    public void LoadFomenu()
    {

        SceneManager.LoadScene(Fomenu);
    }


    //PÁLYAVÁLASZTÓ MENÜ VÉGE-------------------





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
