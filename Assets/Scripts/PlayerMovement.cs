using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5.0f;
    [SerializeField] private Rigidbody rigidbody;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal * speed, 0f, vertical * speed) * Time.deltaTime;
        transform.position += move;
    }
}
