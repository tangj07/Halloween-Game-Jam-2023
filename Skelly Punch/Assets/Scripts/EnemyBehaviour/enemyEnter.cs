using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] float jumpDistanceX = 6, jumpDistanceY=10;
    public Vector3 startPosition;
    public float direction = 1;
    public float enterStart, enterEnd;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
           startPosition = gameObject.transform.position;

            direction = 0;
            if (startPosition.x < 0)//jump right
            {
                direction = jumpDistanceX;
            }
            else if (startPosition.x > 0)//jump left
            {
                direction = -1 * jumpDistanceX;
            }
            if (startPosition.y < 0)//needs to jump higher
            {
                jumpDistanceY += jumpDistanceY * 1.0001f;
            }
            //rb.AddForce(new Vector3(direction, jumpDistanceY), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

/*        if(gameObject.transform.position.x < enterStart  && startPosition.x < 0)
        {
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
        if(gameObject.transform.position.x > enterEnd && startPosition.x > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        }*/
    }
}
