using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    //BELÉPÉS MENÜ-------------------
    //Fõmenü betöltése
    public string Fomenu;

    public void ExitGame()
    {
        // A program bezárása
        Application.Quit();
    }


    public void LoadFomenu()
    {
       
        SceneManager.LoadScene(Fomenu);
    }
    //BELÉPÉS MENÜ VÉGE-------------------




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


    //Kilépés BELEPES menübe
    public string Belepes;

    public void LoadBelepes()
    {

        SceneManager.LoadScene(Belepes);
    }
    //FÕMENÜ MENÜ VÉGE-------------------



    //PÁLYAVÁLASZTÓ MENÜ-------------------
    //Tutorial világ betöltése
    public string Tutorialvilag;

    public void LoadTutorialvilag()
    {

        SceneManager.LoadScene(Tutorialvilag);
    }

    //Kilépés - Fõmenübe

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
