using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    //BEL�P�S MEN�-------------------
    //Bejelentkez�s men� bet�lt�se (bel�p�s gomb)
    public string Bejelentkezes;

    public void LoadBejelentkezes()
    {

        SceneManager.LoadScene(Bejelentkezes);
    }

    public void ExitGame()
    {
        // A program bez�r�sa (kil�p�s gomb)
        Application.Quit();
    }
    //BEL�P�S MEN� V�GE-------------------


    //BEJELENTKEZ�S MEN�-------------------
    //F�men� megnyit�sa (bejelentkez�s gomb)
    //Write.cs Login() met�dus bet�lti a 'Fomenu' scene-t sikeres bejelentkez�s sor�n


    //Kil�p�s BELEPES men�be (kilepes gomb)
    public string Belepes;

    public void LoadBelepes()
    {

        SceneManager.LoadScene(Belepes);
    }
    //BEJELENTKEZ�S MEN� V�GE-------------------



    //F�MEN� MEN�-------------------
    //P�lyav�laszt� bet�lt�se
    public string Palyavalaszto;

    public void LoadPalyavalaszto()
    {

        SceneManager.LoadScene(Palyavalaszto);
    }


    //Be�ll�t�sok bet�lt�se
    public string Beallitasok;

    public void LoadBeallitasok()
    {

        SceneManager.LoadScene(Beallitasok);
    }

    //Eredmenyek menu


   
    //F�MEN� MEN� V�GE-------------------



    //P�LYAV�LASZT� MEN�-------------------
    //Tutorial vil�g bet�lt�se
    public string Tutorialvilag;

    public void LoadTutorialvilag()
    {

        SceneManager.LoadScene(Tutorialvilag);
    }

    //Kil�p�s - F�men�be (kil�p�s gombra)
    public string Fomenu;

    public void LoadFomenu()
    {

        SceneManager.LoadScene(Fomenu);
    }


    //P�LYAV�LASZT� MEN� V�GE-------------------





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
