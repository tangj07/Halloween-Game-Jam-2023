using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Pumpkin : MonoBehaviour
{
    [SerializeField] float gravity;
    [SerializeField] float startSpeed;

    [Header("Explosion")]
    [SerializeField] GameObject explosionFX;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] float damageRadius;
    [SerializeField] float knockBackMagMin;
    [SerializeField] float knockBackMagMax;

    [SerializeField] CinemachineImpulseSource imp;
    [SerializeField] float impForce;

    public Vector2 target;
    private float fallVel;

    private Rigidbody2D rb;

    private void Start()
    {
        fallVel = -startSpeed;
        

        //FallTowardsTarget(target);
    }

    public void FallTowardsTarget(Vector2 pos)
    {
        target = pos;

        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(FallIEnum());
    }

    private IEnumerator FallIEnum()
    {

        while(this.transform.position.y >= target.y )
        {
            fallVel += gravity * Time.deltaTime;
            rb.velocity = new Vector2(0, -fallVel);

            yield return null;
        }

        imp.GenerateImpulse(impForce);

        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, damageRadius, enemyMask);

        for (int i = 0; i < hits.Length; i++)
        {
            ApplyKnock(hits[i]);
        }
        Instantiate(explosionFX, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void ApplyKnock(Collider2D hit)
    {
        // Get direction of knockback based on player direction 
        Vector2 dir = (hit.transform.position - this.transform.position).normalized;

        float knockBackMag = Random.Range(knockBackMagMin, knockBackMagMax);

        hit.GetComponent<EnemyController>().GetHit(
                   knockBackMag,
                   dir // Random between two vectors 
                   );
    }
}
