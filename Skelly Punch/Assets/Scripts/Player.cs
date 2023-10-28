using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundIdealSpeed;
    [SerializeField] float groundSpeedUpRate;
    [SerializeField] float groundSpeedDownRate;

    [SerializeField] float airborneIdealSpeed;
    [SerializeField] float airborneSpeedUpRate;
    [SerializeField] float airborneSpeedDownRate;

    [SerializeField] float jumpForce;

    private Rigidbody2D rb;

    private bool isGrounded;
    private bool isFacingRight; // Just to know if to reset speed 

    private float currentUnitSpeed; // Percent of speed 
    private float currentSpeed;
    public bool FacingRight { get { return isFacingRight; } }
    //public Vector2 GetDir { get { return isFacingRight ? Vector2.right : Vector2.left; } }

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PhysicsMovePlayer();
        SquishSquashPlayer();
    }


    private void PhysicsMovePlayer()
    {
        if(isGrounded)
        {
            GroundMovement();
        }
        else
        {
            AirMovement();
        }

        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
        //rb.MovePosition(rb.position + Vector2.right * currentSpeed);
    }

    private void GroundMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            // Move right 
            if (!isFacingRight)
            {
                currentUnitSpeed = 0;
                currentSpeed = 0;
                isFacingRight = true;
            }

            currentUnitSpeed = Mathf.Clamp01(currentUnitSpeed + groundSpeedUpRate * Time.deltaTime);
            currentSpeed = currentUnitSpeed * groundIdealSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            // Move left 
            if (isFacingRight)
            {
                currentUnitSpeed = 0;
                currentSpeed = 0;
                isFacingRight = false;
            }

            currentUnitSpeed = Mathf.Clamp01(currentUnitSpeed + groundSpeedUpRate * Time.deltaTime);
            currentSpeed = -currentUnitSpeed * groundIdealSpeed;
        }
        else
        {
            // Speed down 
            currentUnitSpeed = Mathf.Clamp01(currentUnitSpeed - groundSpeedDownRate * Time.deltaTime);
            currentSpeed = currentUnitSpeed * (isFacingRight ? groundIdealSpeed : -groundIdealSpeed);
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            // Jumping 
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    private void AirMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            // Move right 
            if (!isFacingRight)
            {
                currentUnitSpeed = 0;
                currentSpeed = 0;
                isFacingRight = true;
            }

            currentUnitSpeed = Mathf.Clamp01(currentUnitSpeed + airborneSpeedUpRate * Time.deltaTime);
            currentSpeed = currentUnitSpeed * airborneIdealSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            // Move left 
            if (isFacingRight)
            {
                currentUnitSpeed = 0;
                currentSpeed = 0;
                isFacingRight = false;
            }

            currentUnitSpeed = Mathf.Clamp01(currentUnitSpeed + airborneSpeedUpRate * Time.deltaTime);
            currentSpeed = -currentUnitSpeed * airborneIdealSpeed;
        }
        else
        {
            // Speed down 
            currentUnitSpeed = Mathf.Clamp01(currentUnitSpeed - airborneSpeedDownRate * Time.deltaTime);
            currentSpeed = currentUnitSpeed * (isFacingRight ? airborneIdealSpeed : -airborneIdealSpeed);
        }
    }

    private void SquishSquashPlayer()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            return;

        if(collision.transform.position.y < this.transform.position.y)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            return;

        if (collision.transform.position.y < this.transform.position.y)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
