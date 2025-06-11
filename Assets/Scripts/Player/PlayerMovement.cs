using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject model;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 3.5f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float dashForce = 5.0f;
    [SerializeField][ReadOnly] private float currentSpeed;
    [SerializeField][ReadOnly] private Vector3 moveInput;
    [SerializeField][ReadOnly] private Vector3 moveVelocity;
    [SerializeField] private bool isSprinting;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDistance = 0.5f;
    [SerializeField][ReadOnly] private bool isGrounded;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleInput();
        HandleJump();
        UpdateAnimator();
    }

    // Frame-rate independent update
    private void FixedUpdate()
    {
        HandleMovement();
    }

    // Handles all inputs that directly affect the player's movement (except jump)
    private void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;

        moveInput = (cameraForward * vertical + cameraRight * horizontal).normalized;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Dash();
        }

        isSprinting = Input.GetKey(KeyCode.LeftShift);
        float speed = isSprinting ? sprintSpeed : walkSpeed;
        moveVelocity = moveInput * speed;
        currentSpeed = moveVelocity.magnitude;
    }

    // Handles all logic revolving around jumping
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, jumpForce, rigidBody.velocity.z);
            animator.SetBool("Jump", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("Jump", false);
        }
    }

    // TODO A very simple dash, need to make more robust
    private void Dash()
    {
        if (moveInput == Vector3.zero) return;

        Vector3 dashDirection = moveInput.normalized;
        rigidBody.AddForce(dashDirection * dashForce, ForceMode.VelocityChange);
    }

    // Handles rigidbody movement based on user input
    private void HandleMovement()
    {
        // Only update horizontal velocity
        Vector3 horizontalVelocity = moveVelocity;
        horizontalVelocity.y = rigidBody.velocity.y;

        rigidBody.velocity = horizontalVelocity;

        // Rotate model toward movement
        if (moveInput != Vector3.zero)
        {
            model.transform.forward = moveInput;
        }
    }

    // State updates for the animator
    private void UpdateAnimator()
    {
        animator.SetBool("Grounded", isGrounded);
        animator.SetFloat("Speed", currentSpeed);
    }


    // TODO Implement!!
    private void OnLand() { }
    private void OnFootstep() { }

}



