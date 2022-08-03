using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    public AudioController theAM;

    private void Awake()
    {
        if(AudioController.instance ==null)
        {
           AudioController newAM = Instantiate(theAM);
           AudioController.instance = newAM;
           DontDestroyOnLoad(newAM.gameObject);
        }
    }
}
