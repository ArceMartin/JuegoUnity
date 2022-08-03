using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    void Awake()
    {
         if(instance==null)
        {
            instance= this;
            DontDestroyOnLoad(gameObject);
        } else
        Destroy(gameObject);
    }

    // Script for the health controller of the player being damaged.
    
    public int playerHp;
    public int maxHp;

    public float invincibilityLength;
    private float invCounter;

    public float flashLength;
    private float flashTime;

    public SpriteRenderer[] playerSprite;
    // Start is called before the first frame update
    void Start()
    {
        playerHp = maxHp;
        

       
    }

    // Update is called once per frame
    void Update()
    {
        if(invCounter >0)
        {
          invCounter-=Time.deltaTime;

          flashTime -=Time.deltaTime;
          if(flashTime <=0)
          {
               foreach(SpriteRenderer sr in playerSprite)
               {
                 sr.enabled = !sr.enabled;
               }
               flashTime = flashLength;
          }

          if(invCounter <=0)
          {
            foreach(SpriteRenderer sr in playerSprite)
               {
                 sr.enabled = true;

               }
               flashTime=0f;
          }
        }
    }

    public void DamagePlayer(int damageReceived)
    {
      if(invCounter<= 0)
      {
        playerHp-=damageReceived;
      if(playerHp<=0)
      {
        playerHp=0;
        //gameObject.SetActive(false);
        RespawnController.instance.Respawn();
        AudioController.instance.PLaySFX(8);
      }else
      {
        invCounter= invincibilityLength;
        AudioController.instance.PLaySFXAdjusted(11);
      }
      UiController.instance.updateHp(playerHp,maxHp);
      }
    }

    public void FillHealth()
    {
       playerHp = maxHp;
       UiController.instance.updateHp(playerHp,maxHp);
    }

    public void HealPlayer(int healAmount)
    {
       playerHp += healAmount;
       if(playerHp > maxHp)
       {
           playerHp= maxHp;
       }
       UiController.instance.updateHp(playerHp,maxHp);
    }
}
