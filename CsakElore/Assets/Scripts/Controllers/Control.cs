using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem.XR;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.IO;


/*játékállapotok 
 * Countdown - visszaszámlálás állapot
 * Playing - játék állapot
 * Paused - szünet állapot*/
public enum PlayerState
{
    Countdown,
    Playing,
    Paused
}

public class Control : MonoBehaviour
{
    //Választott pálya
    public static string selectedLevel;

    //talaj réteg - isOnGroundhoz szükséges
    public LayerMask groundLayer;

    //Kamera vezérlés
    public Transform cam;
    float turnSmoothTime = .1f;
    float turnSmoothVelocity;

    //Karakter vezérlés
    CharacterController controller;
    Vector2 movement;
    public float walkSpeed;
    public float sprintSpeed;
    float trueSpeed;
    public float boostedSpeed;

    //Ugrás
    public float jumpHeight;
    public float gravity;
    bool isGrounded;
    Vector3 velocity;

    //Animátor
    private Animator animator;

    //Idõzítõ - játékosállapot idõ alapján
    public TextMeshProUGUI MainTimerText;
    public TextMeshProUGUI CountdownText;
    public PlayerState playerState = PlayerState.Countdown;
    float mainElapsedTime;
    float countdownDuration = 3f;

    // Mentett adatok
    Vector3 savedPosition;
    float savedTimeScale;


    //Text objektumok
    public TextMeshProUGUI mapFinishText;
    public Button exitButton;

    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    //Adatbázis
    private string connectionString;
    private MySqlConnection MS_Connection;
    private MySqlCommand MS_Command;
    string query;

   

 
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        trueSpeed = walkSpeed;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       
    }

    // Update is called once per frame
    void Update()
    {
       //Szünet
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (playerState == PlayerState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        //Visszaszámlálás
        if (playerState == PlayerState.Countdown)
        {
           
            countdownDuration -= Time.deltaTime;
            CountdownText.text = Mathf.CeilToInt(countdownDuration).ToString();
            if (countdownDuration <= 0)
            {
                CountdownText.text = "";
                playerState = PlayerState.Playing; //Játék állapot
            }
        }
        else if (playerState == PlayerState.Playing)
        {
            // Fõ idõ
            mainElapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(mainElapsedTime / 60);
            int seconds = Mathf.FloorToInt(mainElapsedTime % 60);
            float milliseconds = (mainElapsedTime * 1000) % 1000;
            MainTimerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

            isGrounded = Physics.CheckSphere(transform.position, .1f, groundLayer);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -1;
            }

            //animator változók - animációkhoz, sebességhez

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                bool isWalking = true;
                animator.SetBool("Walk_bool", isWalking);
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                bool isWalking = false;
                animator.SetBool("Walk_bool", isWalking);
            }


            //Shift - sprintelés - animáció - sebesség
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                trueSpeed = sprintSpeed;
                bool isSprint = true;
                animator.SetBool("Sprint_bool", isSprint);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                trueSpeed = walkSpeed;
                bool isSprint = false;
                animator.SetBool("Sprint_bool", isSprint);
                bool isWalking = true;
                animator.SetBool("Walk_bool", isWalking);
            }

            //Irányítás, input kezelés
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * trueSpeed * Time.deltaTime);
            }

            //Ugrás
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
                animator.SetTrigger("Jump_trig");
               
            }

            velocity.y += (gravity * 10) * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            // Karakter Y-koordinátájának ellenõrzése
            if (transform.position.y < -2f)
            {
                // Ha a karakter Y-koordinátája -2 alá esik - zuhanás animáció
                animator.SetBool("Fall_bool", true);
            }
            else
            {
                animator.SetBool("Fall_bool", false);
            }

            // Karakter Y-koordinátájának ellenõrzése
            if (transform.position.y < -100f)
            {
                gameOver();
            }
        }
        else if (playerState == PlayerState.Paused)
        {
            // Ha a játék szüneteltetve van, a mozgást és sebességet letiltjuk
            movement = Vector2.zero;
            velocity = Vector3.zero;
        }
    }



    //Ütközések kezelése
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Boostpad
        if (hit.gameObject.CompareTag("Boostpad"))
        {    
            trueSpeed = boostedSpeed;
        }
        if (!hit.gameObject.CompareTag("Boostpad"))
        {
            trueSpeed = walkSpeed;
        }

        if (hit.gameObject.CompareTag("Obstacle"))
        {
           
            gameOver();
            Debug.Log("Akadály ütközés");
        }


        //Cél
        if (hit.gameObject.CompareTag("Finish"))
        {
            
            Finish();
        }
    }

    //Cél elérése
    void Finish()
    {
        Time.timeScale = 0f;

        //Idõ elmentése
        PlayerPrefs.SetFloat("MainElapsedTime", mainElapsedTime);
        PlayerPrefs.Save();

       //Adatmentés meghívása
        SaveToLocalFile(mainElapsedTime, DateTime.Now);

        //Egyéb változók beállítása
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        mapFinishText.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        Debug.Log("Pálya teljesítve");
        
    }


    // Pályák specifikus idõhatárok
    private Dictionary<string, (TimeSpan arany, TimeSpan ezust, TimeSpan bronz)> idohatarok = new Dictionary<string, (TimeSpan, TimeSpan, TimeSpan)>
    {
        { "Palya1", (TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(5)) },
        { "Palya2", (TimeSpan.FromMinutes(2), TimeSpan.FromMinutes(4), TimeSpan.FromMinutes(6)) },
        { "Palya3", (TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(2.5), TimeSpan.FromMinutes(4)) },
        { "Palya4", (TimeSpan.FromMinutes(3), TimeSpan.FromMinutes(6), TimeSpan.FromMinutes(9)) },
        { "Palya5", (TimeSpan.FromMinutes(2.5), TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(7.5)) }
    };


    //Adatmentés - idõ,dátum,kupa neve
    void SaveToLocalFile(float elapsedTime, DateTime date)
    {
        // Bejelentkezett felhasználó nevének lekérése - fájl névhez
        string loggedInUserName = PlayerPrefs.GetString("LoggedInUsername");

        // Elmenteni kívánt adatok formázása stringgé - megf. formátum
        string formattedElapsedTime = TimeSpan.FromSeconds(elapsedTime).ToString(@"mm\:ss\:fff");
        string formattedDate = date.ToString("yyyy.MM.dd");





        // Adatok összeállítása stringgé
        string dataToSave = formattedElapsedTime + "," + formattedDate;

        // Fájlnév: bejelentkezettfelh_pálya_savedData.txt és elérési út:perzisztens mappa (AppData -> LocalLow -> DefaultCompany -> CsakElore) megadása 
        string fileName = $"{loggedInUserName}_{Control.selectedLevel}_savedData.txt";
        string directoryPath = Application.persistentDataPath;
        string filePath = Path.Combine(directoryPath, fileName);

        // Adatok mentése a fájlba
        System.IO.File.WriteAllText(filePath, dataToSave);
    }


    //Játék vége - pl.: akadály ütközés, leesés
    void gameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    //Szünet - ESC - Beallitasok scene
    void PauseGame()
    {
        savedTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        savedPosition = transform.position;
        playerState = PlayerState.Paused;
        SceneManager.LoadScene("Beallitasok", LoadSceneMode.Additive);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    //Folytatás - ESC

    void ResumeGame()
    {
        Time.timeScale = savedTimeScale;
        transform.position = savedPosition;
        playerState = PlayerState.Playing;
        SceneManager.UnloadSceneAsync("Beallitasok");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Játék újrakezdése pl.: GameOver() esetén
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
