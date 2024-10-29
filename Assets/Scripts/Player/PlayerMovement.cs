using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField][ReadOnly] private float currentSpeed;

    [Header("General")]
    [SerializeField][ReadOnly] private Vector3 move;

    [Header("Grounded")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private float speed = 3.5f;
    [SerializeField] private float dashDistance = 5.0f;
    [SerializeField] private bool isSprinting;



    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] public float groundDistance = 0.5f;

    [Header("Animations")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject model;

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
        GetMove();
        isGrounded = IsGrounded();
        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            speed = 6.0f;
            Sprinting();
 
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isDashing();
        }
        else
        {
            isSprinting = false;
            speed = 3.5f;
            Walking();
        }
        
        animator.SetBool("Grounded", isGrounded);
        animator.SetFloat("Speed", currentSpeed);



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
        // Apply movement
        if (move != Vector3.zero)
        {
            model.transform.forward = move.normalized;
            transform.position += move;
        }
    }

    void Sprinting()
    {
        // Apply movement
        if (move != Vector3.zero)
        {
            model.transform.forward = move.normalized;
            transform.position += move;
        }
    }

    void OnLand()
    {
        // Used for Animator, will hopefully implement special conditions for landing later
    }

    void OnFootstep()
    {
        // Used for Animator
    }



    Vector3 GetMove() 
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var targetAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;


        currentSpeed = new Vector3(horizontal, 0, vertical).normalized.magnitude * speed;
        

        // Get the camera's forward and right vectors, ignoring any vertical direction
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;

        // Calculate the movement direction based on input and camera's orientation
        move = (cameraForward * vertical + cameraRight * horizontal).normalized * currentSpeed * Time.deltaTime;

        return move;
    }

    // Temporary Dash function, to be removed upon completion of the smoother dash
    void isDashing()
    {
        transform.position += GetMove().normalized * dashDistance;
    }
}
