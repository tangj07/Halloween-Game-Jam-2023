using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] public float timeUntilHurt = 0.05f;
    [SerializeField] bool isProj = false;
    private GameManager manager;

    private void Start()
    {
        manager = GameObject.FindAnyObjectByType<GameManager>();
    }

    public float timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            if (isProj == true)
            {
                timer = 0;
                manager.EndGame();
            }
        }
            
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print(collision.gameObject.name);

        if(collision.gameObject.tag == "Player")
        {
            timer += Time.deltaTime;
            if (timer > timeUntilHurt)
            {
                timer = 0;
                manager.EndGame();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timer = 0;
        }
    }
}
