using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20;


    public float horizontalInput;
    public float forwardInput;
    public float turnSpeed;

    private Rigidbody playerRb;

    public float jumpForce;
    public float gravityModifier;

    

    public bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        

    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");


        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;
    }
}
