using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float speed = 7.5f;
    [SerializeField] private GameObject model;
    [SerializeField][ReadOnly] private float currentSpeed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    
    void Update()
    {
        
    }


    void Sprinting()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var targetAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;


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
            model.transform.forward = move.normalized;
            transform.position += move;
        }
    }
}

