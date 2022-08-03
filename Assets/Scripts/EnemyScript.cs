using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Set points of the patrol points 
    public Transform[] patrolPoints;
    private int currentPoint;
    
    public float moveSpeed, waitAtPoint;
    private float waitCounter;

    public float jumpForce;
    public Rigidbody2D rb;
  //  public int health=100;

    public GameObject deathEffect;
    public Animator anim;


    void Start()
    {
       waitCounter=waitAtPoint;

       // Eliminate the relationship of the patrolpoints with the enemy.
       foreach(Transform pPoint in patrolPoints) pPoint.SetParent(null);
       
    }

    void Update()
    {

      // Control of the movement of the enemy
      if(Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x)>.2f)
      {
         // if the condition is true the enemy will be walking to the right else to the left.
         if(transform.position.x < patrolPoints[currentPoint].position.x)
         {
            rb.velocity= new Vector2(moveSpeed,rb.velocity.y);
            transform.localScale=new Vector3(-1f,1f,1f);
         } else
         {
            rb.velocity= new Vector2(-moveSpeed,rb.velocity.y);
            transform.localScale= new Vector3(1f,1f,1f);
         }

          if(transform.position.y< patrolPoints[currentPoint].position.y -.1f && rb.velocity.y <.1f)
          {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
          }
      }else
      {
        rb.velocity= new Vector2(0f,rb.velocity.y);

        waitCounter -= Time.deltaTime;
        if(waitCounter<=0)
        {
          waitCounter= waitAtPoint;
          currentPoint++;

        if(currentPoint>=patrolPoints.Length)
        currentPoint=0;

        }

      }

      anim.SetFloat("Speed",Mathf.Abs(rb.velocity.x));


    }
    /*
    public void takeDamage (int damage)
    {
        health  -= damage;
        if(health <= 0)
        {
           Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }*/
}
