using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipDirOnMove : MonoBehaviour
{
    public Transform playerObject;
    private Transform transform;
    private SpriteRenderer spriteRenderer;  //sprite Renderer component 
    
    void Start()
    {
        //playerObject = transform.Find("pf_PlayerCamp");
        transform = this.GetComponent<Transform>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        
    }
}
