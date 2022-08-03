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

    public float dashSpeed, dashTime;
    private float dashCounter;
    private float currentSpeed = 0;
    private bool isOnGround;

    // Making reference of the Bullet for the player to use.
    public Bullet shotFire;

    // Making a point for the player shoots.
    public Transform firePoint;
    // Identifying the ground level.
    public Transform GroundPoint;
    // Making one Layer for the ground.
    public LayerMask WhatIsGround;
    // var that allow us to set an animator.
    public Animator anim,ballAnim;

    //jump var.
    private bool canDoubleJump;

    // Implementing Gameobject to the player in ball or standing form.
    public GameObject standing,ball;

    // Timers to the change of forms.
    public float waitToChange;
    private float ballCounter;
    private float bombCounter;
    private float bombRecharge;
    // Implementing SpriteRenderers for dash animation and his limits.
    public SpriteRenderer SR, afterImage;
    public float afterImageLifetime, timeBetweenAfterImage;
    private float afterImageCounter;
    public Color afterImageColor;
    public float waitAfterDash; 
    private float DashRecharge;

    // Implementing Bombs for the player in ball form.
    public Transform BombPoint;
    public GameObject Bomb;
    

    [Header("Booleanos")]
    public bool puedoMover = true; // Limitara el movimiento al personaje
    public bool moviendo = false; //Variable para saber si se está en movimiento o no.
    
    // Reference to the script with the AbilityTracker of the player.
    private PlayerAbilityTracker abilities;

    public bool canMoveOnGate;

    void Start()
    {
      abilities= GetComponent<PlayerAbilityTracker>();
      canMoveOnGate= true;
    }

    // Update is called once per frame
    void Update()
    {
      if(canMoveOnGate && Time.timeScale!=0)
      {
      if(bombCounter>0)
      {
        bombCounter=bombCounter-Time.deltaTime;
        bombRecharge=0.009f;
      }
      
       if(dashCounter>0)
       {
            // Dash de player
             dashCounter=dashCounter-Time.deltaTime;
             rbody.velocity= new Vector2(dashSpeed* transform.localScale.x ,rbody.velocity.y);
             afterImageCounter -= Time.deltaTime;
             if(afterImageCounter <= 0)
             {
                 ShowAfterImage();
             }
             DashRecharge= waitAfterDash;
       }
       else
       {
        // Movimiento del player
         rbody.velocity = new Vector2(currentSpeed, rbody.velocity.y);

         // Cambio de dirección de player
         if(rbody.velocity.x<0)
         {transform.localScale = new Vector3(-0.17f,0.17f,1f);}
         else if(rbody.velocity.x>0)
         {
         transform.localScale = new Vector3(0.17f,0.17f,1f);
         }
       }

        
        
      }
      else rbody.velocity = Vector2.zero;

          // Se encarga de observar si el player esta en suelo
         isOnGround = Physics2D.OverlapCircle(GroundPoint.position,.05f,WhatIsGround);

        if(standing.activeSelf)
        {
          anim.SetBool("IsOnGround",isOnGround);
          anim.SetFloat("Speed",Mathf.Abs(rbody.velocity.x));
        }

        if(ball.activeSelf)
        {
          ballAnim.SetFloat("Speed",Mathf.Abs(rbody.velocity.x));
        }
         
            
    }

     private void OnJump() 
     {
            // Aplica fuerza de salto
            if(isOnGround==true || (canDoubleJump && abilities.hasDoubleJump) && canMoveOnGate && Time.timeScale!=0)
            {
                if(isOnGround && !ball.activeSelf )
                {
                  canDoubleJump=true;
                  AudioController.instance.PLaySFXAdjusted(12);
                }
                else
                {
                   canDoubleJump=false; 
                   anim.SetTrigger("DoubleJump");
                   AudioController.instance.PLaySFXAdjusted(9);
                }
                

                rbody.velocity = new Vector2(rbody.velocity.x, jumpForce);
            }
            
        
    }

    // Shooting key.
    private void OnFire1()
    {
      if(standing.activeSelf && canMoveOnGate && Time.timeScale!=0) 
      {
        Shoot();
        
      }
        else if(ball.activeSelf && abilities.hasBombAttack && canMoveOnGate)
        {
          if(bombRecharge>0) bombRecharge-=Time.deltaTime;
          else 
          {
            bombCounter=0.01f;
            BombActive();
          }
        }
    }

    


    // Dashing key.
    private void OnFire2()
    {
      if(abilities.hasDash && canMoveOnGate && Time.timeScale!=0)
      {
        if(DashRecharge>0)  DashRecharge-=Time.deltaTime;
        else
        {
            if(standing.activeSelf)
            {
            dashCounter=dashTime;
            ShowAfterImage(); 
            AudioController.instance.PLaySFXAdjusted(7);
            } 
        }
      }
        
    }

    void Shoot()
    {
       Instantiate(shotFire, firePoint.position, firePoint.rotation).moveDir= new Vector2(transform.localScale.x,0f);
       anim.SetTrigger("ShotFired");
       AudioController.instance.PLaySFXAdjusted(14);
    }

    void BombActive()
    {
      Instantiate(Bomb,BombPoint.position,BombPoint.rotation);
      AudioController.instance.PLaySFXAdjusted(13);
    }

    // Change to Ball key.
    private void OnBall()
    {
      if(abilities.hasBallForm && canMoveOnGate && Time.timeScale!=0)
      {
         ballCounter-=Time.deltaTime;
          if(ballCounter<=0 )
          {
               ball.SetActive(true);
               standing.SetActive(false);
               AudioController.instance.PLaySFX(6);
          }
          else ballCounter=waitToChange;
      }
    }

    private void OnStand()
    {
      if(abilities.hasBallForm && canMoveOnGate && Time.timeScale!=0)
      {
        ballCounter-=Time.deltaTime;
          if(ballCounter<=0 )
          {
               ball.SetActive(false);
               standing.SetActive(true);
               AudioController.instance.PLaySFX(10);
          }
          else ballCounter=waitToChange;
      }
      

    }
     
     
     private void OnMove(InputValue inputValue) 
     {
       if(canMoveOnGate && Time.timeScale!=0)
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
        else if(moveValue==0)
        {
           
            moviendo=false;
        }
         
        if(moveValue!=0)
        {
           
            moviendo=true;
           
        }  
       }
    }
    
     private void OnPause()
    {
        UiController.instance.pauseState();
    }
   
    public void ShowAfterImage()
    {
       SpriteRenderer image=  Instantiate(afterImage,transform.position, transform.rotation);
       image.sprite = SR.sprite;
       image.transform.localScale= transform.localScale;
       image.color=afterImageColor;

       Destroy(image.gameObject,afterImageLifetime);
       afterImageCounter=timeBetweenAfterImage;
    }
}
