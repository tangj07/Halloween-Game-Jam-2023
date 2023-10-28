using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] float idealSpeed;
    [SerializeField] float speedUpRate;

    [SerializeField] float knockTime;
    [SerializeField] float reboundForceHorizontalMin;
    [SerializeField] float reboundForceHorizontalMax;
    [SerializeField] float reboundForceVerticalMin;
    [SerializeField] float reboundForceVerticalMax;

    [Header("Death")]
    [SerializeField] float fallG;
    [SerializeField] float fallDrag;
    [SerializeField] float upwardForceMin;
    [SerializeField] float upwardForceMax;
    [SerializeField] SpriteRenderer spriteRenderer;

    [SerializeField] Vector3 targetScale;
    [SerializeField] float changeRateScale;
    [SerializeField] AnimationCurve scaleCurve;
    [SerializeField] float angleMin = -20.0f;
    [SerializeField] float angleMax = 20.0f;
    [SerializeField] float changeRateRot;
    [SerializeField] AnimationCurve rotCurve;
    [SerializeField] Color colorTint;
    [SerializeField] float changeRateColor;

    private Rigidbody2D rb;
    private Player player;

    private bool isGrounded;
    private bool isFacingRight;
    private float currentUnitSpeed; // Percent of speed 
    private float currentSpeed;

    private bool isKnocked;
    private bool knockedRight;
    private bool dead;

    private Coroutine knockCo;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindObjectOfType<Player>();
    }

    private void Update()
    {
        //rb.AddForce((player.transform.position - this.transform.position).normalized * moveForce);
        
        if(!isKnocked && !dead)
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
        if (dead)
            return;

        if(isKnocked)
        {
            // Reset time knocked if hit again 
            StopCoroutine(knockCo);
        }

        rb.AddForce(knockBack * dir, ForceMode2D.Impulse);
        health -= 1;

        if(health <= 0)
        {
            dead = true;

            this.GetComponent<Collider2D>().enabled = false;
            rb.gravityScale = fallG;
            rb.drag = fallDrag;
            rb.AddForce(Vector2.up * Random.Range(upwardForceMin, upwardForceMax), ForceMode2D.Impulse);

            StartCoroutine(DeathIEnumScale(changeRateScale, targetScale));
            StartCoroutine(DeathIEnumRot(changeRateRot, Random.Range(angleMin, angleMax)));
            StartCoroutine(DeathIEnumColor(changeRateColor, colorTint));
        }

        knockCo = StartCoroutine(KnockIEnum());
        isKnocked = true;
        knockedRight = this.transform.position.x > player.transform.position.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            if(isKnocked)
            {
                rb.AddForce((knockedRight ? Vector2.left : Vector2.right) * Random.Range(reboundForceHorizontalMin, reboundForceHorizontalMax), ForceMode2D.Impulse);
            }
            
            //rb.velocity = new Vector2(-rb.velocity.x * 3.0f, rb.velocity.y);
            return;
        }

        if (collision.transform.position.y < this.transform.position.y)
        {
            isGrounded = true;

            if(isKnocked)
            {
                rb.AddForce(Vector2.up * Random.Range(reboundForceVerticalMin, reboundForceVerticalMax), ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            //rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
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

    private IEnumerator DeathIEnumScale(float rate, Vector3 target)
    {
        Vector3 startScale = this.transform.localScale;

        float lerp = 0.0f;
        while(lerp <= 1.0f)
        {

            this.transform.localScale = Vector3.Lerp(startScale, target, scaleCurve.Evaluate(lerp));

            lerp += rate * Time.deltaTime;
            yield return null; 
        }
    }

    private IEnumerator DeathIEnumRot(float rate, float target)
    {
        float startRot = this.transform.eulerAngles.z;

        float lerp = 0.0f;
        while (lerp <= 1.0f)
        {

            this.transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(startRot, target, rotCurve.Evaluate(lerp)));

            lerp += rate * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator DeathIEnumColor(float rate, Color target)
    {
        Color startColor = spriteRenderer.color;
        float lerp = 0.0f;
        while (lerp <= 1.0f)
        {

            spriteRenderer.color = Color.Lerp(startColor, target, lerp);

            lerp += rate * Time.deltaTime;
            yield return null;
        }
    }
}
