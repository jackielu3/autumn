using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uwu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D player;
    [SerializeField] private Transform feetPos;
    public bool canMove;

    [Header("Grounded")]
    public bool isGrounded;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float playerSpeed;


    private Vector2 move;

    [Header("Jump")]
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;

    private bool isJumping;
    private float jumpTimeCounter;

    [Header("Animations")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
        

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }


    void FixedUpdate()
    {
        // Checks if player can move and changes the velocity of the player if so
        if (canMove)
        {
            player.velocity = new Vector2(move.x * playerSpeed * Time.deltaTime, player.velocity.y);

            if (player.velocity.y < maxFallSpeed)
            {
                player.velocity = new Vector2(player.velocity.x, maxFallSpeed);
            }
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        } 

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(player.velocity.x));

 
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        animator.SetFloat("Direction", Input.GetAxisRaw("Horizontal"));

        // Checks if the player is supposed to be able to move and then sets grounded if so
        if (canMove)
        {
            isGrounded = Physics2D.OverlapCircle(feetPos.position, .2f, ground);
        }
        else
        {
            isGrounded = false;
        }
        // Basic jump
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            player.velocity = Vector2.up * jumpForce;
        }

        // "Long" Held down jump check
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                player.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

    }

    public void SetCanMove(bool Bool)
    {
        canMove = Bool;
    }

}