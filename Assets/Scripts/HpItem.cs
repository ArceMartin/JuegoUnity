using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpItem : MonoBehaviour
{
    public int healthAmount;
    public GameObject pickUpEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.tag=="Player")
      {
        PlayerHealthController.instance.HealPlayer(healthAmount);

        if(pickUpEffect!=null)
        Instantiate(pickUpEffect,transform.position,transform.rotation);
      
        Destroy(gameObject);

        AudioController.instance.PLaySFX(5);
      }
      
    }
}
