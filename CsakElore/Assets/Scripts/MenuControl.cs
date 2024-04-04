using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    //BEL�P�S MEN�-------------------
    //F�men� bet�lt�se
    public string Fomenu;

    public void ExitGame()
    {
        // A program bez�r�sa
        Application.Quit();
    }


    public void LoadFomenu()
    {
       
        SceneManager.LoadScene(Fomenu);
    }
    //BEL�P�S MEN� V�GE-------------------




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


    //Kil�p�s BELEPES men�be
    public string Belepes;

    public void LoadBelepes()
    {

        SceneManager.LoadScene(Belepes);
    }
    //F�MEN� MEN� V�GE-------------------



    //P�LYAV�LASZT� MEN�-------------------
    //Tutorial vil�g bet�lt�se
    public string Tutorialvilag;

    public void LoadTutorialvilag()
    {

        SceneManager.LoadScene(Tutorialvilag);
    }

    //Kil�p�s - F�men�be

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
