using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runspeed = 25f;
    float horizontalmove = 0f;
    bool jump = false;
    bool crouch = false;
    public Animator animator;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {

        horizontalmove = Input.GetAxisRaw("Horizontal") * runspeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalmove)); 

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
      
    }
    void FixedUpdate()
    {
        controller.Move(horizontalmove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        
    }
}
