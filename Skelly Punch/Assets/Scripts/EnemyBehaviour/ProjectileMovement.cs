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
    float direction=0;
    float throwDistanceX = 5, throwDistanceY = 15;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        startPosition = player.transform.position;
        
        if (startPosition.x < 0)
        {
            direction = -1 * throwDistanceX;
        }
        else
        {
            direction = throwDistanceX;
        }
        rb.AddForce(new Vector3(direction, throwDistanceY) , ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //out of bounds
        if (gameObject.transform.position.x<leftWall||
            gameObject.transform.position.x>rightWall||
            gameObject.transform.position.y>topWall||
            gameObject.transform.position.y<bottomWall) 
        { Destroy(gameObject); }
        
    }
}
