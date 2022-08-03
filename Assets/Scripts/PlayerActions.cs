using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    public Rigidbody2D rbody;

    [Header("Estadisticas")]
    public float jumpForce=10;
    public float walkSpeed=10;
    private float currentSpeed = 0;
    private bool isOnGround;

    public Bullet shotFire;

    public Transform firePoint;
    public Transform GroundPoint;
    public LayerMask WhatIsGround;
    public Animator anim;

    

    [Header("Booleanos")]
    public bool puedoMover = true; // Limitara el movimiento al personaje
    public bool moviendo = false; //Variable para saber si se está en movimiento o no.
    

    // Update is called once per frame
    void Update()
    {

        // Movimiento del player
         rbody.velocity = new Vector2(currentSpeed, rbody.velocity.y);

         // Cambio de dirección de player
         if(rbody.velocity.x<0)
         {transform.localScale = new Vector3(-0.17f,0.17f,1f);}
         else if(rbody.velocity.x>0)
         {
         transform.localScale = new Vector3(0.17f,0.17f,1f);;
         }
         // Se encarga de observar si el player esta en suelo
         isOnGround = Physics2D.OverlapCircle(GroundPoint.position,.2f,WhatIsGround);

         anim.SetBool("IsOnGround",isOnGround);
         anim.SetFloat("Speed",Mathf.Abs(rbody.velocity.x));
            
    }

     private void OnJump() 
     {
       
            
            // Aplica fuerza de salto
            if(isOnGround)
            rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
        
    }

    private void OnFire1()
    {
        Shoot();
        
    }

    void Shoot()
    {
       Instantiate(shotFire, firePoint.position, firePoint.rotation).moveDir= new Vector2(transform.localScale.x,0f);
       anim.SetTrigger("ShotFired");
    }

     private void OnMove(InputValue inputValue) 
     {
        float moveValue = inputValue.Get<float>();
      
        // Aplica velocidad al movimiento
        currentSpeed = moveValue * walkSpeed;
        
        

        
 
        if(moveValue < 0 && transform.localScale.x > 0)
        {
           
           
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            moviendo=true;
        }
        else if(moveValue > 0 && transform.localScale.x < 0)
        {
           
            
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            moviendo = true;
        }
        else
        {
           
            moviendo=false;
        }
         
        if(moveValue!=0)
        {
           
            moviendo=true;
           
        }  

       

        
 
 
    }

   
}
