using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ActionPhysics : MonoBehaviour {

    private Inputs input;
    private Rigidbody2D player;

    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;   // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private int numberOfJumps = 0;
    [SerializeField] private int maxJumps = 1;

    const float k_GroundedRadius = 0.0145f;                                         // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;                                                    // Whether or not the player is grounded.
    const float k_CeilingRadius = 0.2f;                                          // Radius of the overlap circle to determine if the player can stand up
    private bool m_FacingRight = true;                                          // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    public float runspeed = 25f;
    float horizontalmove = 0f;

    public Animator animator;
    [SerializeField] AudioSource jumpFX;
    [SerializeField] AudioSource WalkFX;
    private UnityEvent OnLandEvent;

    Vector2 movement;

    private void Awake()
    {
        input = new Inputs();
        player = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void Update()
    {
        groundCheck();
        jump();
        move();
        Debug.Log(numberOfJumps);
    }

    private void jump()
    {
        if (input.Gameplay.Jump.triggered && numberOfJumps > 0)
        {
            jumpFX.Play();
            m_Grounded = false;
            player.AddForce(new Vector2(0f, m_JumpForce));
            numberOfJumps--;
        }
    }

    private void move()
    {
        movement = input.Gameplay.Movement.ReadValue<Vector2>();
        horizontalmove = movement.x * runspeed * Time.deltaTime;
        animator.SetFloat("Speed", Mathf.Abs(horizontalmove));

        if ((m_Grounded || m_AirControl) && horizontalmove != 0)
        {

            WalkFX.Play();

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(horizontalmove * 10f, player.velocity.y);
            // And then smoothing it out and applying it to the character
            player.velocity = Vector3.SmoothDamp(player.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (horizontalmove > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (horizontalmove < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
    }

    private void groundCheck()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                    numberOfJumps = maxJumps;
                }
            }
        }
        if (!m_Grounded)
        {
            animator.SetBool("Jumping", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnEnable()
    {
        input.Gameplay.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.Disable();
    }
}
