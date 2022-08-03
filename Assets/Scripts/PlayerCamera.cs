using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private PlayerActions player;
    public BoxCollider2D boundsBox;

    private float halfHeight, halfWidth;
    // Start is called before the first frame update
    void Start()
    {
       player= FindObjectOfType<PlayerActions>(); 

    
    halfHeight = Camera.main.orthographicSize;
    halfWidth = halfHeight * Camera.main.aspect;

    AudioController.instance.PLayLevelMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if(player !=null)
        {
            transform.position= new Vector3(
            Mathf.Clamp(player.transform.position.x,boundsBox.bounds.min.x +halfWidth,boundsBox.bounds.max.x -halfWidth),
            Mathf.Clamp(player.transform.position.y,boundsBox.bounds.min.y +halfHeight,boundsBox.bounds.max.y -halfHeight),
            transform.position.z);
        } else
          player= FindObjectOfType<PlayerActions>();
    }
}
