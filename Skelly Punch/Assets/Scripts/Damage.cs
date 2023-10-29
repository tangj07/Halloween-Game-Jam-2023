using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] float timeUntilHurt = 0.05f;
    private GameManager manager;

    private void Start()
    {
        manager = GameObject.FindAnyObjectByType<GameManager>();
    }

    private float timer;
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
