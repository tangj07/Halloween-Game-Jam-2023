using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] float idealSpeed;
    [SerializeField] float speedUpRate;

    [SerializeField] float knockTime;

    private Rigidbody2D rb;
    private Player player;

    private bool isGrounded;
    private bool isFacingRight;
    private float currentUnitSpeed; // Percent of speed 
    private float currentSpeed;

    private bool isKnocked;

    private Coroutine knockCo;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<Player>();
    }

    private void Update()
    {
        //rb.AddForce((player.transform.position - this.transform.position).normalized * moveForce);
        
        if(!isKnocked)
        {
            Move();
        }
    }

    private void Move()
    {
        if (isGrounded)
        {
            bool nextFace = player.transform.position.x > this.transform.position.x;

            // Reset speed if changing direction 
            if (nextFace != isFacingRight)
            {
                currentUnitSpeed = 0.0f;
                isFacingRight = !isFacingRight;
            }

            // Speed up
            currentUnitSpeed = Mathf.Clamp01(currentUnitSpeed + speedUpRate * Time.deltaTime);
            currentSpeed = currentUnitSpeed * (isFacingRight ? idealSpeed : -idealSpeed);

            // Set vel 
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        }
        else
        {
            // Reset speed 
            currentUnitSpeed = 0.0f;
            currentSpeed = 0.0f;
        }
    }

    public void GetHit(float knockBack, Vector2 dir)
    {
        if(isKnocked)
        {
            // Reset time knocked if hit again 
            StopCoroutine(knockCo);
        }

        rb.AddForce(knockBack * dir, ForceMode2D.Impulse);
        health -= 1;

        knockCo = StartCoroutine(KnockIEnum());
        isKnocked = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            return;
        }

        if (collision.transform.position.y < this.transform.position.y)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
            return;
        }

        if (collision.transform.position.y < this.transform.position.y)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }


    private IEnumerator KnockIEnum()
    {
        yield return new WaitForSeconds(knockTime);
        isKnocked = false;
    }
}
