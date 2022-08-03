using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    private PlayerCamera cam;
    public Animator anim;
    public Transform camPos;
    public Transform boss;
    public float camSpeed;

    public int threshold1,threshold2;

    public float activeTime, fadeoutTime, standbyTime;
    private float activeCounter, fadeCounter, standbyCounter;

    public Transform[] spawnpoint;
    private Transform targetPoint;
    public float moveSpeed;

    public float timeBetweenShots1,timeBetweenShots2;
    private float shotCounter;
    public GameObject bullet;
    public Transform shotPoint;

    public GameObject winObject;

    private bool battleEnded;
    public string bossRef;
    // Start is called before the first frame update
    void Start()
    {
        cam= FindObjectOfType<PlayerCamera>();
        cam.enabled = false;

        activeCounter= activeTime;
        shotCounter= timeBetweenShots1;

        AudioController.instance.PlayBossMusic();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position= Vector3.MoveTowards(cam.transform.position,camPos.position,camSpeed * Time.deltaTime);
        if(!battleEnded)
        {
        if(BossHealthController.instance.currentHp > threshold1)
        {
            if(activeCounter >0)
            {
                 activeCounter -= Time.deltaTime;
                 if(activeCounter <=0)
                 {
                     fadeCounter = fadeoutTime;
                     anim.SetTrigger("Vanish");
                 }
                 shotCounter -= Time.deltaTime;
                 if(shotCounter<=0) 
                 { shotCounter = timeBetweenShots1;
                   Instantiate(bullet,shotPoint.position,Quaternion.identity);
                 }

            }else if(fadeCounter >0)
            {
                fadeCounter  -= Time.deltaTime;
                if(fadeCounter <=0)
                 {
                     boss.gameObject.SetActive(false);
                     standbyCounter= standbyTime;
                 }
            }else if(standbyCounter >0)
            {
                standbyCounter -= Time.deltaTime;
                if(standbyCounter<=0)
                {
                     boss.position= spawnpoint[Random.Range(0,spawnpoint.Length)].position;
                     boss.gameObject.SetActive(true);

                     activeCounter= activeTime;
                     shotCounter = timeBetweenShots1;
                }
            }
             
        } else 
        {
            if(targetPoint == null)
            {
                targetPoint = boss;
                fadeCounter= fadeoutTime;
                anim.SetTrigger("Vanish");
            }else
            { // Inicio Segunda Fase 
                 if(Vector3.Distance(boss.position,targetPoint.position)>.02f)
                 {
                    boss.position= Vector3.MoveTowards(boss.position,targetPoint.position, moveSpeed * Time.deltaTime);
                 activeCounter -= Time.deltaTime;
                 if(Vector3.Distance(boss.position,targetPoint.position)<=.02f)
                 {
                     fadeCounter = fadeoutTime;
                     anim.SetTrigger("Vanish");
                 }
                   
                 shotCounter -= Time.deltaTime;
                 if(shotCounter<=0) 
                 { 
                   if(PlayerHealthController.instance.playerHp>threshold2)
                   {
                     shotCounter = timeBetweenShots1;
                   } else  shotCounter = timeBetweenShots2;
                    
                   Instantiate(bullet,shotPoint.position,Quaternion.identity);

                 }

                 }else if(fadeCounter >0)
                {
                fadeCounter  -= Time.deltaTime;
                if(fadeCounter <=0)
                 {
                     boss.gameObject.SetActive(false);
                     standbyCounter= standbyTime;
                 }
                 }else if(standbyCounter >0)
                 {    
                standbyCounter -= Time.deltaTime;
                 if(standbyCounter<=0)
                {
                     boss.position= spawnpoint[Random.Range(0,spawnpoint.Length)].position;
                     
                     
                     targetPoint =spawnpoint[Random.Range(0,spawnpoint.Length)];
int flag=0;
                     while(targetPoint.position == boss.position && flag<100)
                     {
                        targetPoint = spawnpoint[Random.Range(0,spawnpoint.Length)];
                        flag++;
                     }
                     

                     boss.gameObject.SetActive(true);

                      if(PlayerHealthController.instance.playerHp>threshold2)
                   {
                     shotCounter = timeBetweenShots1;
                   } else  shotCounter = timeBetweenShots2;
                   }
                 }
            }
        }
      }
      else
      {
            fadeCounter -= Time.deltaTime;
            if(fadeCounter <0)
            {
                if(winObject != null)
                {
                    winObject.SetActive(true);
                    winObject.transform.SetParent(null);
                    cam.enabled= true;
                    gameObject.SetActive(false);
                    AudioController.instance.PLayLevelMusic();
                    PlayerPrefs.SetInt(bossRef,1);
                }
            }
      }
    }

    public void endBattle()
    {
        battleEnded = true;
        fadeCounter = fadeoutTime;
        anim.SetTrigger("Vanish");
        boss.GetComponent<Collider2D>().enabled=false;

        BossBullet[] bullets = FindObjectsOfType<BossBullet>();
        if(bullets.Length >0)
        {
            foreach(BossBullet bb in bullets) Destroy(bb.gameObject);
        }
    }
}
