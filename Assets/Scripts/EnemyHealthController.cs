using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    // Start is called before the first frame update
    public int enemyHp=5;

    public GameObject deathEffect;

    public void damageEnemy(int damageReceived)
    {
       enemyHp-=damageReceived;
       if(enemyHp<=0)
       {
          if(deathEffect!=null)
          Instantiate(deathEffect,transform.position,transform.rotation);
          Debug.Log("Enemy Defeated");
          Destroy(gameObject);

          AudioController.instance.PLaySFX(4);
       }
       
    }
}
