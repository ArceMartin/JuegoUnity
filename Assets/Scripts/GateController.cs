using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateController : MonoBehaviour
{
    public Animator anim;
    public float distanceToOpen;

    private PlayerActions thePlayer;

    private bool playerExiting;

    public Transform exitPoint;
    public float movePlayerSpeed;

    public string loadLevel;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer= PlayerHealthController.instance.GetComponent<PlayerActions>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,thePlayer.transform.position)< distanceToOpen)
        {
           anim.SetBool("doorOpen",true);
        }
        else anim.SetBool("doorOpen",false);

        if(playerExiting)
        {
           thePlayer.transform.position = Vector3.MoveTowards(thePlayer.transform.position,exitPoint.position,movePlayerSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
           if(!playerExiting)
           {
            thePlayer.canMoveOnGate=false;
            StartCoroutine(useGateCo());
           }
        }
    }

    IEnumerator useGateCo()
    {
        playerExiting=true;
        thePlayer.anim.enabled=false;

        UiController.instance.StartFading();

        yield return new WaitForSeconds(1.5f);

        RespawnController.instance.SetSpawn(exitPoint.position);
        thePlayer.canMoveOnGate = true;
        thePlayer.anim.enabled= true;

        UiController.instance.StopFading();

        PlayerPrefs.SetString("ContinueLevel",loadLevel);
        PlayerPrefs.SetFloat("PosX",exitPoint.position.x);
        PlayerPrefs.SetFloat("PosY",exitPoint.position.y);
        PlayerPrefs.SetFloat("PosZ",exitPoint.position.z);
        SceneManager.LoadScene(loadLevel);
    }
}
  