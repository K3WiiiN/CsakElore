using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
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


    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
    


        animator = GetComponent<Animator>();


        trueSpeed = walkSpeed;


        controller = GetComponent<CharacterController>();

        //Kurzor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    // Update is called once per frame
    void Update()
    {

        /*Debug üzenetek
        Debug.Log(isGrounded);
        Debug.Log("Ugrás input érzékelése: " + Input.GetButtonDown("Jump"));
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("'Jump_trig' aktiválódik");
        }
        Debug.Log("Animator állása: " + animator.GetCurrentAnimatorStateInfo(0).fullPathHash);
        Debug.Log("Ugrás animáció lejátszódik:  " + animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"));
        if (GetComponent<Animation>() == null)
        {
            Debug.LogError("Animáció komponens nincs a játékoson");
        }
        */


        isGrounded = Physics.CheckSphere(transform.position, .1f, groundLayer);
        //isGrounded = Physics.CheckSphere(transform.position, 0.1f, 1);
        //isGrounded = Physics.Raycast(transform.position, -Vector3.up, 0.1f,1);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1;
        }

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
            // Ha a karakter Y-koordinátája -2 alá esik, akkor lejátszuk a zuhanás animációját
            animator.SetBool("Fall_bool", true);
        }
        else
        {
            animator.SetBool("Fall_bool", false);
        }

        



    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {


        if (hit.gameObject.CompareTag("Boostpad"))
        {
            trueSpeed = boostedSpeed;
        }
        if (!hit.gameObject.CompareTag("Boostpad"))
        {
            trueSpeed = walkSpeed;
        }

    }



}