using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float moveSpeed;

    public Rigidbody2D rb;
    public int damageAmount;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir = transform.position-PlayerHealthController.instance.transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;  // Convert angle to Deg instead of rad
        transform.rotation= Quaternion.AngleAxis(angle,Vector3.forward);
        AudioController.instance.PLaySFXAdjusted(2);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = -transform.right * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       if(other.gameObject.tag == "Player")
       {
         PlayerHealthController.instance.DamagePlayer(damageAmount);
       }

       if(impactEffect != null)
       {
         Instantiate(impactEffect,transform.position,transform.rotation);
         Destroy(gameObject);
       }

       AudioController.instance.PLaySFXAdjusted(3);
    }
}
