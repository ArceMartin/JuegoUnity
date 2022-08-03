using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public string NewGameScene;

    public GameObject continueBtn;

    public PlayerAbilityTracker pat;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("ContinueLevel"))
        {
            continueBtn.SetActive(true);
        }
        AudioController.instance.PLayMainMenuMusic();
    }

    public void NewGame()
    {
      PlayerPrefs.DeleteAll();
      SceneManager.LoadScene(NewGameScene);
    }

    public void Continue()
    {
      pat.gameObject.SetActive(true);
      pat.transform.position = new Vector3(PlayerPrefs.GetFloat("PosX"),PlayerPrefs.GetFloat("PosY"),PlayerPrefs.GetFloat("PosZ"));
     
      if(PlayerPrefs.HasKey("DJUnlocked"))
      {
        if(PlayerPrefs.GetInt("DJUnlocked")==1)
        {
            pat.hasDoubleJump=true;
        }
      }

      if(PlayerPrefs.HasKey("DashUnlocked"))
      {
        if(PlayerPrefs.GetInt("DashUnlocked")==1)
        {
            pat.hasDash=true;
        }
      }

      if(PlayerPrefs.HasKey("BallUnlocked"))
      {
        if(PlayerPrefs.GetInt("BallUnlocked")==1)
        {
            pat.hasBallForm=true;
        }
      }

      if(PlayerPrefs.HasKey("BombUnlocked"))
      {
        if(PlayerPrefs.GetInt("BombUnlocked")==1)
        {
            pat.hasBombAttack=true;
        }
      }

     SceneManager.LoadScene(PlayerPrefs.GetString("ContinueLevel"));
    }

    public void QuitGame()
    {
       Application.Quit();
       Debug.Log("Game Closed");
    }
}
