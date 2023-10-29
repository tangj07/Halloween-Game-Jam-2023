using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    [SerializeField] float disToExplode = 0.5f;
    [SerializeField] float gravity;
    [SerializeField] float startSpeed;

    private Vector2 target;
    private float fallVel;

    private Rigidbody2D rb;

    private void Start()
    {
        fallVel = -startSpeed;
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void FallTowardsTarget(Vector2 pos)
    {
        target = pos;
        StartCoroutine(FallIEnum());
    }

    private IEnumerator FallIEnum()
    {
        float sqrtDis = Vector2.Distance(this.transform.position, target);

        while(sqrtDis > disToExplode )
        {
            fallVel += gravity * Time.deltaTime;
            rb.velocity = new Vector2(0, -fallVel);

            yield return null;
        }
    }
}
