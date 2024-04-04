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

        // Karakter Y-koordin�t�j�nak ellen�rz�se
        if (transform.position.y < -100f)
        {
            gameOver();
        }




    }

    //J�t�k v�ge
    void gameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    //P�lya teljes�tve
    void Finish()
    {
        mapFinishText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        Debug.Log("P�lya teljes�tve");
    }

    //J�t�k �jrakezd�se
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

   

    //�tk�z�s objektummal
    void OnControllerColliderHit(ControllerColliderHit hit)
    {


        if (hit.gameObject.CompareTag("Obstacle"))
        {
            gameOver();
            Debug.Log("Akad�ly �tk�z�s");
        }

       
        


        if (hit.gameObject.CompareTag("Finish"))
        {
            Finish();
        }
    }


      

}
