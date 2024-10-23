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

        currentSpeed = new Vector3(horizontal, 0, vertical).magnitude * speed;

        // Get the camera's forward and right vectors, ignoring any vertical direction
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;

        // Calculate the movement direction based on input and camera's orientation
        Vector3 move = (cameraForward * vertical + cameraRight * horizontal).normalized * speed * Time.deltaTime;

        // Log to check the values of cameraForward, cameraRight, and move
        Debug.Log($"CameraForward: {cameraForward}, CameraRight: {cameraRight}, Move: {move}");

        // Apply movement
        if (move != Vector3.zero)
        {
            model.transform.forward = new Vector3(move.x, 0, move.z).normalized;
            transform.position += move;
        }
    }



    void OnLand()
    {
        // Used for Animator, will hopefully implement special conditions for landing later
    }
}
