using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5.0f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] public float groundDistance = 0.5f;
    [SerializeField] private Rigidbody rigidBody;

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
