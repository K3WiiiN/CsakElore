using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem.XR;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    public TextMeshProUGUI mapFinishText;
    public Button exitButton;

   

    void Start()
    {

    }

    void Update()
    {

        // Karakter Y-koordinátájának ellenõrzése
        if (transform.position.y < -100f)
        {
            gameOver();
        }




    }

    //Játék vége
    void gameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    //Pálya teljesítve
    void Finish()
    {
        mapFinishText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        Debug.Log("Pálya teljesítve");
    }

    //Játék újrakezdése
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

   

    //Ütközés objektummal
    void OnControllerColliderHit(ControllerColliderHit hit)
    {


        if (hit.gameObject.CompareTag("Obstacle"))
        {
            gameOver();
            Debug.Log("Akadály ütközés");
        }

       
        


        if (hit.gameObject.CompareTag("Finish"))
        {
            Finish();
        }
    }


      

}
