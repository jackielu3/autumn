using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rigidBody;
    private float currentSpeed;

    [Header("Grounded")]
    private float speed = 5.0f;

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
        currentSpeed = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z).magnitude;
        animator.SetFloat("Speed", currentSpeed);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal * speed, 0f, vertical * speed) * Time.deltaTime;
        transform.position += move;
        if (Input.GetAxis("Mouse X") < 0)
        {
            //Code for action on mouse moving left
            print("Mouse moved left");
        }
        if (Input.GetAxis("Mouse X") > 0)
        {
            //Code for action on mouse moving right
            print("Mouse moved right");
        }



        if (Input.GetKeyDown("space") && IsGrounded())
        {
            rigidBody.velocity = Vector3.up * jumpForce;
        }
    }
}
