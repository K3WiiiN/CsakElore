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


    //Ugrás
    public float jumpHeight;
    public float gravity;
    bool isGrounded;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(isGrounded);
        trueSpeed = walkSpeed;

        controller = GetComponent<CharacterController>();

        //Kurzor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, .1f, groundLayer);
        //isGrounded = Physics.CheckSphere(transform.position, 0.1f, 1);
        //isGrounded = Physics.Raycast(transform.position, -Vector3.up, 0.1f,1);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1;
        }



        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            trueSpeed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            trueSpeed = walkSpeed;
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
        }

            velocity.y += (gravity * 10) * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        
        
    }       
    
}
