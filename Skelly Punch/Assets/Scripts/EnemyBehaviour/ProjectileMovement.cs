using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    public float topWall, bottomWall, rightWall, leftWall;
    Vector3 startPosition;
    private Player player;
    public Rigidbody2D rb;
    float direction=1;
    float jumpDistanceX = 2, jumpDistanceY = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        startPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        direction = 0;
        //Vector3 position = transform.position, velocity = new Vector3(speed * Time.deltaTime, 0, 0);
        if (startPosition.x < 0)
        {
            //position -= velocity;
            direction = jumpDistanceX;
        }
        else
        {
            //position += velocity;
            direction = -1 * jumpDistanceX;
        }
        //transform.position = position;
        rb.AddForce(new Vector3(direction, jumpDistanceY) , ForceMode2D.Impulse);
        //out of bounds
        if (gameObject.transform.position.x<leftWall||
            gameObject.transform.position.x>rightWall||
            gameObject.transform.position.y>topWall||
            gameObject.transform.position.y<bottomWall) 
        { Destroy(gameObject); }
        
    }
}
