using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMovement : MonoBehaviour
{
    [SerializeField]
    float speed;

    [HideInInspector]
    public float x, y, topWall, bottomWall, rightWall, leftWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //Vector3 position = transform.position, velocity = new Vector3(0, speed * Time.deltaTime, 0);
       //position += transform.rotation * velocity;
       //transform.position = position; time -= Time.deltaTime;
       ////out of bounds
       //if (gameObject.transform.position.x<width) { Destroy(gameObject); }
        
    }
}
