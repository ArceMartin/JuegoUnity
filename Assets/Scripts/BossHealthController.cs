using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public static BossHealthController instance;

    private void Awake()
    {
        instance= this;
    }

    public Slider bossHpUI;

    public int currentHp;

    public BossBattle theBoss;


    // Start is called before the first frame update
    void Start()
    {
        bossHpUI.maxValue= currentHp;

        bossHpUI.value= currentHp;
    }

    public void takeDamage(int damageReceived)
    {
      currentHp -= damageReceived;
      if(currentHp <= 0)
      {
          currentHp =0;
          theBoss.endBattle();
          AudioController.instance.PLaySFX(0);
          
      }else AudioController.instance.PLaySFX(1);
      bossHpUI.value= currentHp;

    }

    
}
