using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float BulletSpeed;
    public int damage=1;
    public Rigidbody2D rb;
    public Vector2 moveDir;
    public GameObject impactEffect;

    // Start is called before the first frame update
    
    void Update()
    {
      rb.velocity = moveDir * BulletSpeed;
    }
     
    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.tag=="Enemy")
      {
        other.GetComponent<EnemyHealthController>().damageEnemy(damage);
      }

      if(other.tag=="Boss")
      {
        BossHealthController.instance.takeDamage(damage);
      }

      /*  EnemyScript enemy =  other.GetComponent<EnemyScript>();
        if (enemy!= null)
        {
               enemy.takeDamage(damage);
        }*/
        if(impactEffect!=null)
        {
          Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        
          
        AudioController.instance.PLaySFXAdjusted(3);
        Debug.Log(other.name);
        Destroy(gameObject);
    }
    
    void OnBecameInvisible()
    {
      Destroy(gameObject);
    }
}
