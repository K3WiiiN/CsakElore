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
    public string Eredmenyek;

    public void LoadEredmenyek()
    {
        SceneManager.LoadScene(Eredmenyek);
    }

   
    //FÕMENÜ MENÜ VÉGE-------------------



    //PÁLYAVÁLASZTÓ MENÜ-------------------

    //Tutorial világ betöltése
    public string Tutorialvilag;

    public void LoadTutorialvilag()
    {

        SceneManager.LoadScene(Tutorialvilag);
    }

    //Elsõ pálya betöltése
    public string Palya1;
   

    public void LoadPalya1()
    {

        SceneManager.LoadScene(Palya1);
        Control.selectedLevel = "Palya1";
    }

    //Második pálya betöltése
    public string Palya2;

    public void LoadPalya2()
    {
        SceneManager.LoadScene(Palya2);
        Control.selectedLevel = "Palya2";
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
