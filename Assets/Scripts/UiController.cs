using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UiController : MonoBehaviour
{
    public static UiController instance;

    private void Awake()
    {
         if(instance==null)
        {
            instance= this;
        DontDestroyOnLoad(gameObject);
        } else
        Destroy(gameObject);
    }

    public Slider hpSlider;

    public Image fadeScreen;
    public float fadeSpeed=2f;
    private bool fadeToBlack,fadingFromBlack;

    public string MainMenuScene;

    public GameObject pauseScreen;
    // Start is called before the first frame update
    void Start()
    {
      updateHp(PlayerHealthController.instance.maxHp,PlayerHealthController.instance.maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeToBlack)
        {
         fadeScreen.color= new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a,1f,fadeSpeed* Time.deltaTime));
        if(fadeScreen.color.a==1f) fadeToBlack=false;
        } else if(fadingFromBlack)
        {
         fadeScreen.color= new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a,0f,fadeSpeed* Time.deltaTime));
         if(fadeScreen.color.a==0f) fadingFromBlack=false;
        }

    
    }

    public void updateHp(int currentHp, int maxHp)
    {
      hpSlider.maxValue=maxHp;
      hpSlider.value=currentHp;
    }

    public void StartFading()
    {
        fadeToBlack= true; 
        fadingFromBlack=false;
    }

    public void StopFading()
    {
        fadeToBlack= false;
        fadingFromBlack=true; 
      
    }

    public void pauseState()
    {
       if(!pauseScreen.activeSelf) 
       {
        pauseScreen.SetActive(true);
        Time.timeScale= 0f;
       }
       else 
       {
       pauseScreen.SetActive(false); 
       Time.timeScale= 1f;
       }
    } 

    public void goToMainMenu()
    {
      

      Destroy(PlayerHealthController.instance.gameObject);
      PlayerHealthController.instance = null;

      Destroy(RespawnController.instance.gameObject);
      RespawnController.instance=null;

      instance= null;
      Destroy(gameObject);
      Time.timeScale= 1f;
      SceneManager.LoadScene(MainMenuScene);
       
    }

    
}
