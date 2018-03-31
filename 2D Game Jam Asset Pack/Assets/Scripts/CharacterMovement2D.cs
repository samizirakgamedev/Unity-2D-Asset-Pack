using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour {

    [SerializeField][Tooltip("Input the name of the boolean being used in the Animator to trigger your characters jump animation")]
    private string jumpBoolName;

    [SerializeField][Tooltip("Array of empty GameObject Transforms (must be made children of the character and positioned at characters feet) that are used to calculate when the player touches the ground")]
    private Transform[] groundPoints;

    [SerializeField]
    [Range(0, 15)]
    [Tooltip("The speed at which your character will move along the X Axis")]
    private float movementSpeed = 7.0f;

    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Creates invisible radius around whatever you choose is 'Ground'. This is used to detect when the player is 'Grounded'")]
    private float groundRadius = 0.2f;

    [SerializeField]
    [Range(0, 15)]
    private float jumpForce = 10.0f;

    [SerializeField][Range(0,5)]
    private float fallMultiplier = 2.5f;

    [SerializeField][Range(0,5)]
    public float lowJumpMultiplier = 3.0f;

    [SerializeField]
    private LayerMask whatIsGround;

    private Rigidbody2D playerRigidbody;

    private Animator playerAnimator;

    private bool facingDefault;

    private bool isGrounded;

    private bool jump;

    private bool jumpEnded;

    private bool landedAudioPlayed;

    [SerializeField]
    private bool airControl;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        landedAudioPlayed = false;
    }


    void Update()
    {
        HandleInput();

        Debug.Log("It is: " + jumpEnded);
    }

    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();

        Movement(horizontal);

        FlipPlayer(horizontal);

        ResetValues();

    }

    private void Movement(float horizontal)
    {
        if ((isGrounded || airControl))
        {
            playerRigidbody.velocity = new Vector2(horizontal * movementSpeed, playerRigidbody.velocity.y);
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
            playerAnimator.SetBool(jumpBoolName, true);
            SoundManager.PlaySound("PlayerJump");
        }

        if(jumpEnded)
        {
            if(playerRigidbody.velocity.y > lowJumpMultiplier)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, lowJumpMultiplier);
            }

            jumpEnded = false;
        }
    }

    private void FlipPlayer(float horizontal)
    {
        if (horizontal < 0 && !facingDefault || horizontal > 0 && facingDefault)
        {
            facingDefault = !facingDefault;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            jump = true;

        if (Input.GetButtonUp("Jump") && !isGrounded)
            jumpEnded = true;

        if (isGrounded == false)
            landedAudioPlayed = false;
    }

    private bool IsGrounded()
    {
        if (playerRigidbody.velocity.y <= 0)
        {
            playerRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        playerAnimator.SetBool(jumpBoolName, false);

                        if (landedAudioPlayed == false)
                        {
                            SoundManager.PlaySound("PlayerLand");
                            landedAudioPlayed = true;
                        }
                        
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void ResetValues()
    {
        jump = false;
        jumpEnded = false;
    }
}
