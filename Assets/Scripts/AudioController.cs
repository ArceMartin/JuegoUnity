using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    private void Awake()
    {
        if(instance==null)
        {
           instance=this;
           DontDestroyOnLoad(gameObject);
        }else
        {
           Destroy(gameObject);
        }
    } 
    
    // Access to the audioSource on the folder of music and sfx for the game.
    public AudioSource mainMenuMusic,lvlMusic,bossMusic;
    public AudioSource[] sfx;

    public void PLayMainMenuMusic()
    {
        
            lvlMusic.Stop();
            bossMusic.Stop();
            mainMenuMusic.Play();
        
        
    }

    public void PLayLevelMusic()
    { 
      if(!lvlMusic.isPlaying)
      {
      bossMusic.Stop();
      mainMenuMusic.Stop();
      lvlMusic.Play();
      }
    }

    public void PlayBossMusic()
    {
      lvlMusic.Stop();
      bossMusic.Play();
    }    
    
    public void PLaySFX(int sfxNum)
    {
        sfx[sfxNum].Stop();
        sfx[sfxNum].Play();
    }

    public void PLaySFXAdjusted(int sfxAjust)
    {
       sfx[sfxAjust].pitch = Random.Range(.8f,1.2f);
       PLaySFX(sfxAjust);
    }
}
