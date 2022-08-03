using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AbilityUnlockable : MonoBehaviour
{

    public bool unlockDoubleJump,unlockDash,unlockBallForm,unlockBomb;

    public GameObject pickUpEffect;

    public string unlockMsg;
    public TMP_Text unlockText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
          PlayerAbilityTracker player= other.GetComponentInParent<PlayerAbilityTracker>();

          if(unlockDoubleJump) 
          {
            player.hasDoubleJump=true;
            PlayerPrefs.SetInt("DJUnlocked",1);
          }

          if(unlockDash) 
          {
            player.hasDash=true;
            PlayerPrefs.SetInt("DashUnlocked",1);
          
          }

          if(unlockBallForm) 
          {
            player.hasBallForm=true;
            PlayerPrefs.SetInt("BallUnlocked",1);

          }
          if(unlockBomb) 
          {
            player.hasBombAttack=true;
            PlayerPrefs.SetInt("BombUnlocked",1);
          }
          Instantiate(pickUpEffect,transform.position,transform.rotation);
          
          unlockText.transform.parent.SetParent(null);
          unlockText.transform.parent.position= transform.position;

          unlockText.text = unlockMsg;
          unlockText.gameObject.SetActive(true);

          Destroy(unlockText.transform.parent.gameObject,5f);
          Destroy(gameObject);

          AudioController.instance.PLaySFX(5);
        }
        
    }
}
