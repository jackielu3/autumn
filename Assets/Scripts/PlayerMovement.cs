using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField][ReadOnly] private float currentSpeed;

    [Header("Grounded")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float speed = 5.0f;


    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] public float groundDistance = 0.5f;

    [Header("Animations")]
    [SerializeField] private Animator animator;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDistance);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = IsGrounded();
        Walking();
        animator.SetFloat("Speed", currentSpeed);
        
        animator.SetBool("Grounded", isGrounded);


        if (Input.GetAxis("Mouse X") < 0)
        {
            //Code for action on mouse moving left
            // print("Mouse moved left");
        }
        if (Input.GetAxis("Mouse X") > 0)
        { 
            //Code for action on mouse moving right
            // print("Mouse moved right");
        }



        if (Input.GetKeyDown("space") && isGrounded)
        {
            rigidBody.velocity = Vector3.up * jumpForce;
            animator.SetBool("Jump", true);
        }
        else
        {
            if (isGrounded)
            {
                animator.SetBool("Jump", false);
            }
        }
    }

    void Walking()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal * speed, 0f, vertical * speed) * Time.deltaTime;
        transform.position += move;

        currentSpeed = new Vector3(horizontal, 0, vertical).magnitude * speed;
    }
    
    void OnLand()
    {
        // Used for Animator, will hopefully implement special conditions for landing later
    }
}
