using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D : MonoBehaviour {

    [Header("Ground & Wall Check Settings")]
    [SerializeField]
    [Tooltip("Array of empty GameObject Transforms (must be made children of the character and positioned at characters feet) that are used to calculate when the player touches the ground")]
    private Transform[] groundPoints;

    [SerializeField]
    [Range(0, 1)]
    [Tooltip("Creates invisible radius around whatever you choose is 'Ground'. This is used to detect when the player is 'Grounded'")]
    private float groundRadius = 0.2f;

    [Tooltip("The start and end points of the 3 front facing sensors that are attached to the character. Best made by using Empty GameObjects")]
    [SerializeField]
    private Transform frontCheckStartOne, frontCheckEndOne, frontCheckStartTwo, frontCheckEndTwo, frontCheckStartThree, frontCheckEndThree;

    [Tooltip("The start and end points of the 3 back facing sensors that are attached to the character. Best made by using Empty Gameobjects")]
    [SerializeField]
    private Transform backCheckStartOne, backCheckEndOne, backCheckStartTwo, backCheckEndTwo, backCheckStartThree, backCheckEndThree;

    [Header("Movement Settings")]
    [SerializeField]
    [Range(0, 15)]
    [Tooltip("The speed at which your character will move along the X Axis")]
    private float movementSpeed = 7.0f;

    [SerializeField]
    [Tooltip("The force that is applied to make the character jump")]
    [Range(0, 15)]
    private float jumpForce = 10.0f;

    [Tooltip("The higher the Fall Multiplier the faster the character will fall after reaching the peak of its jump")]
    [SerializeField][Range(0,5)]
    private float fallMultiplier = 2.5f;

    [Tooltip("The Chracters threshold velocity that player has to reach when holding down the jump button to make the character do a high jump rather than a low jump")]
    [SerializeField][Range(0,5)]
    public float lowJumpMultiplier = 3.0f;

    [SerializeField]
    [Tooltip("Input the name of the boolean being used in the Animator to trigger your characters jump animation")]
    private string jumpBoolName;

    [Tooltip("The layer that all objects the character can walk along are tagged as")]
    [SerializeField]
    private LayerMask whatIsGround;

    [Tooltip("If ticked it will allow the player to control the characters movement mid jump")]
    [SerializeField]
    private bool airControl;

    private Rigidbody2D playerRigidbody;

    private Animator charcterAnimator;

    private int groundLayerMask; 

    private bool facingDefault;

    private bool isGrounded;

    private bool jump;

    private bool jumpEnded;

    private bool landedAudioPlayed;

    private bool nullifiedMaterial2D;

    private Collider2D castedCollider2D;

    private PhysicsMaterial2D material2D;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        charcterAnimator = GetComponent<Animator>();
        groundLayerMask = LayerMask.GetMask("Ground");
        material2D = Resources.Load<PhysicsMaterial2D>("Materials/Smooth");

        landedAudioPlayed = false;
        nullifiedMaterial2D = false;
    }


    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");

        isGrounded = IsGrounded();

        Movement(horizontal);

        FlipPlayer(horizontal);

        WallCollisionChecks();

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
            charcterAnimator.SetBool(jumpBoolName, true);
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


    public void WallCollisionChecks()
    {
        RaycastHit2D frontCheckOne = Physics2D.Linecast(frontCheckStartOne.position, frontCheckEndOne.position,groundLayerMask);
        RaycastHit2D frontCheckTwo = Physics2D.Linecast(frontCheckStartTwo.position, frontCheckEndTwo.position, groundLayerMask);
        RaycastHit2D frontCheckThree = Physics2D.Linecast(frontCheckStartThree.position, frontCheckEndThree.position, groundLayerMask);

        RaycastHit2D backCheckOne = Physics2D.Linecast(backCheckStartOne.position, backCheckEndOne.position, groundLayerMask);
        RaycastHit2D backCheckTwo = Physics2D.Linecast(backCheckStartTwo.position, backCheckEndTwo.position, groundLayerMask);
        RaycastHit2D backCheckThree = Physics2D.Linecast(backCheckStartThree.position, backCheckEndThree.position, groundLayerMask);

        if (frontCheckOne | frontCheckTwo | frontCheckThree | backCheckOne | backCheckTwo | backCheckThree)
        {  

            if(frontCheckOne.collider != null)
            {
                castedCollider2D = frontCheckOne.collider;
                castedCollider2D.sharedMaterial = material2D;
                nullifiedMaterial2D = false;
            }

            if (frontCheckTwo.collider != null)
            {
                castedCollider2D = frontCheckTwo.collider;
                castedCollider2D.sharedMaterial = material2D;
                nullifiedMaterial2D = false;
            }

            if (frontCheckThree.collider != null)
            {
                castedCollider2D = frontCheckThree.collider;
                castedCollider2D.sharedMaterial = material2D;
                nullifiedMaterial2D = false;
            }

            if (backCheckOne.collider != null)
            {
                castedCollider2D = backCheckOne.collider;
                castedCollider2D.sharedMaterial = material2D;
                nullifiedMaterial2D = false;
            }

            if (backCheckTwo.collider != null)
            {
                castedCollider2D = backCheckTwo.collider;
                castedCollider2D.sharedMaterial = material2D;
                nullifiedMaterial2D = false;
            }

            if (backCheckThree.collider != null)
            {
                castedCollider2D = backCheckThree.collider;
                castedCollider2D.sharedMaterial = material2D;
                nullifiedMaterial2D = false;
            }
        }

        if (castedCollider2D != null && frontCheckOne == false && frontCheckTwo == false && frontCheckThree == false && backCheckOne == false && backCheckTwo == false && backCheckThree == false && nullifiedMaterial2D == false)
        {
            castedCollider2D.sharedMaterial = null;
            nullifiedMaterial2D = true;
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
                        charcterAnimator.SetBool(jumpBoolName, false);

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
