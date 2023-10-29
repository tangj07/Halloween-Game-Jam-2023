using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Player player;
    Collider2D collide;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        collide = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.down)
        {
            //collide.enabled = false;
            //Physics2D.IgnoreLayerCollision(7, 9,true);
            //gameObject.layer = 0;
            Debug.Log("dsnjasndfla");
        }
        else
        {
            //gameObject.layer = 9;
            //collide.enabled=true;
        }
    }

}
