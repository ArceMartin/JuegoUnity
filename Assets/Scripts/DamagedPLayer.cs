using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedPLayer : MonoBehaviour
{
    public int damageReceived=1;

    public bool destroyOnDamage;
    public GameObject destroyEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
       if(other.gameObject.tag=="Player")
        {
          DealDamage();  
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
          DealDamage();  
        }
      
    }

    void DealDamage()
    {
      PlayerHealthController.instance.DamagePlayer(damageReceived);

      if(destroyOnDamage)
      {
        if(destroyEffect!=null)
        {
          Instantiate(destroyEffect,transform.position,transform.rotation);
        }
         Destroy(gameObject);
        }
    }
}
