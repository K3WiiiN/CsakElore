using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //Mozgás
    public float speed = 30;
    public float turnspeed;
    public Animation runAnim;


    //Input
    private float horizontalInput;
    private float verticalInput;


    //Ugrás
    private Rigidbody rb;
    public float jumpForce;
    public float gravityModifier;
    private bool isOnGround = true;

    //Animáció
    private Animator playerAnim;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);

        transform.Rotate(Vector3.up * turnspeed * horizontalInput * Time.deltaTime);

        
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            
        }
       

    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
