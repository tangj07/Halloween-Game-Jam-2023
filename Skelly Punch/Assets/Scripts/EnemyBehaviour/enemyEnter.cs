using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool jumpIn = true;
    float jumpDistanceX = 6, jumpDistanceY=10;
    public Vector3 startPosition;
    public float direction = 1;
    // Start is called before the first frame update
    void Start()
    {
           startPosition = gameObject.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpIn)
        {
            direction = 0;
            if (startPosition.x <0)//jump right
            {
                direction = jumpDistanceX;
            }
            else if(startPosition.x >0)//jump left
            {
                direction = -1 * jumpDistanceX;
            }
           //if (startPosition.y < 0)//needs to jump higher
           //{
           //    jumpDistanceY += jumpDistanceY * 1.1f;
           //}
            rb.AddForce(new Vector3(direction, jumpDistanceY), ForceMode2D.Impulse);
            jumpIn = false;
        }
    }
}
