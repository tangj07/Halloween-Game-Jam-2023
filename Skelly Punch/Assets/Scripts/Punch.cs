using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Punch : MonoBehaviour
{
    [Header("Collision Detection")]
    [SerializeField] LayerMask enemyLayer;
    [Tooltip("How much the rects are offsetted in opposite x directions and y direction")]
    [SerializeField] Vector2 checkOffset;
    [SerializeField] Vector2 checkRect;

    [Header("Hitting")]
    [SerializeField] float knockBackMagCommon;
    [SerializeField] Vector2 knockbackMinDir;
    [SerializeField] Vector2 knockbackMaxDir;
    [SerializeField] float normalPunchCooldown;

    [SerializeField] Player player;

    [Header("Punch Effects")]
    [SerializeField] PunchStates punchState;
    
    [SerializeField] int punchesBeforeChange;
    [Space]
    [SerializeField] GameObject AOEVisual;
    [SerializeField] float AOERange = 2.0f;
    [SerializeField] float AOEKnockBackMag;
    [Space]
    [SerializeField] float pumpkinTimeDelayMin;
    [SerializeField] float pumpkinTimeDelayMax;
    [Space]
    [SerializeField] float speedyKnockBackMag;
    [Space]
    [SerializeField] float stickTime;

    private PunchStates holdState;
    private enum PunchStates
    {
        AOE,
        PumpkinDropper,
        Speed,
        Web
    }

    // How much knockback 
    private float knockBackMag;

    // Timers for punching 
    private float punchCoolDown;
    private float coolDownTimer;

    private int punchCounter;


    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<Player>();

        holdState = punchState;

        knockBackMag = knockBackMagCommon;
        punchCoolDown = normalPunchCooldown;

        SwapPunchState(punchState);
    }

    // Update is called once per frame
    void Update()
    {
        // Swap effect if next state 
        if(holdState != punchState)
        {
            SwapPunchState(punchState);
            holdState = punchState;
        }

        PunchLogic();
    }

    private void PunchLogic()
    {
        // Can't punch on cooldown 
        if (coolDownTimer > 0)
        {
            coolDownTimer -= Time.deltaTime;
            return;
        }
            

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll((Vector2)this.transform.position + (player.FacingRight ? checkOffset : new Vector2(-checkOffset.x, checkOffset.y)), checkRect, 0, enemyLayer);
            for (int i = 0; i < hitEnemies.Length; i++)
            {
                // Apply current effects to punch-e's
                ApplyPunchEffect(punchState, hitEnemies);
                
                // Apply normal punch logic 
                CommonPunch(hitEnemies[i]);
            }

            // Reset timer 
            coolDownTimer = punchCoolDown;
        }
    }

    /// <summary>
    /// Punch effect that always happens 
    /// </summary>
    private void CommonPunch(Collider2D hit)
    {
        // Get direction of knockback based on player direction 
        Vector2 dir = player.FacingRight ?
            Vector2.Lerp(knockbackMinDir, knockbackMaxDir, Random.Range(0.0f, 1.0f)) :
            Vector2.Lerp(new Vector2(-knockbackMinDir.x, knockbackMinDir.y), new Vector2(-knockbackMaxDir.x, knockbackMaxDir.y), Random.Range(0.0f, 1.0f));
        dir = dir.normalized;

        hit.GetComponent<EnemyController>().GetHit(
                   knockBackMag,
                   dir // Random between two vectors 
                   );
    }

    /// <summary>
    /// When just beginning a new punch state 
    /// </summary>
    /// <param name="nextState"></param>
    private void SwapPunchState(PunchStates nextState)
    {
        // Remove current effects 
        RemovePunchEffect(holdState);

        switch (nextState)
        {
            case PunchStates.AOE:
                knockBackMag = AOEKnockBackMag;
                break;
            case PunchStates.PumpkinDropper:
                break;
            case PunchStates.Speed:
                knockBackMag = speedyKnockBackMag;
                break;
            case PunchStates.Web:
                break;
        }
    }

    /// <summary>
    /// When hitting enemies what logic is played on the hit event 
    /// </summary>
    /// <param name="state"></param>
    /// <param name="hits"></param>
    private void ApplyPunchEffect(PunchStates state, Collider2D[] hits)
    {
        switch (state)
        {
            case PunchStates.AOE:
                ApplyAOE(hits);
                break;
            case PunchStates.PumpkinDropper:
                break;
            case PunchStates.Speed:
                break;
            case PunchStates.Web:
                break;
        }
    }

    private void ApplyAOE(Collider2D[] hits)
    {
        Vector3 pos = hits[0].transform.position + (Vector3.up / 2.0f);

        // Visual 
        GameObject temp = Instantiate(AOEVisual, pos, Quaternion.identity);
        temp.transform.localScale = new Vector3(AOERange, AOERange, 1.0f);

        Collider2D[] explosionHits = Physics2D.OverlapCircleAll(pos, AOERange, enemyLayer);
        for (int e = 0; e < explosionHits.Length; e++)
        {
            if (!hits.Contains(explosionHits[e])) 
            {
                // Apply extra hit for enemies caught in blast
                // but also make sure to not repeat enemies 

                CommonPunch(explosionHits[e]);
            }
            else
            {
                print("Already getting hit");
            }
        }
    }

    /// <summary>
    /// When swapping a punch effect is there any necessary cleaup
    /// </summary>
    /// <param name="state"></param>
    private void RemovePunchEffect(PunchStates state)
    {
        switch (state)
        {
            case PunchStates.AOE:
                knockBackMag = knockBackMagCommon;
                break;
            case PunchStates.PumpkinDropper:
                break;
            case PunchStates.Speed:
                knockBackMag = knockBackMagCommon;
                break;
            case PunchStates.Web:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        // Right
        Gizmos.color = player.FacingRight ? Color.green : Color.white;
        DrawCheckRect(checkOffset, checkRect);

        // Left
        Gizmos.color = !player.FacingRight ? Color.green : Color.white;
        DrawCheckRect(new Vector2(-checkOffset.x, checkOffset.y), checkRect);
    }

    private void DrawCheckRect(Vector2 offset, Vector2 rect)
    {
        Gizmos.DrawWireCube((Vector2)this.transform.position + offset, rect);
    }
}
