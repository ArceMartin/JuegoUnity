using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float CounterBomb=.1f;
    public GameObject explosion;

    public float blastRange;
    public LayerMask whatIsDestructible;

    public int damagePerBomb;
    public LayerMask WhatIsDamageable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CounterBomb -=Time.deltaTime;
        if(CounterBomb<=0)
        {
            if(explosion!=null)
            {
               Instantiate(explosion,transform.position,transform.rotation);
            }
            Destroy(gameObject);

           Collider2D[] objectsToRemove =  Physics2D.OverlapCircleAll(transform.position,blastRange,whatIsDestructible);
           
           if(objectsToRemove.Length >0)
           {
             foreach(Collider2D col in objectsToRemove)
             {
                Destroy(col.gameObject);
             }
           }
        
        Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position,blastRange,WhatIsDamageable);
         
          foreach( Collider2D col in objectsToDamage)
          {
            EnemyHealthController enemyHp = col.GetComponent<EnemyHealthController>();
            if(enemyHp != null)
            {
                enemyHp.damageEnemy(damagePerBomb);
            }
          }
          AudioController.instance.PLaySFXAdjusted(4);
        }
    }
}
