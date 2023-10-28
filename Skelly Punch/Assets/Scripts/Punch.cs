using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    [SerializeField] LayerMask enemyLayer;

    [Tooltip("How much the rects are offsetted in opposite x directions and y direction")]
    [SerializeField] Vector2 checkOffset;
    [SerializeField] Vector2 checkRect;

    [Header("Hitting")]
    [SerializeField] float knockBackMag;
    [SerializeField] Vector2 knockbackMinDir;
    [SerializeField] Vector2 knockbackMaxDir;

    [SerializeField] Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll((Vector2)this.transform.position + (player.FacingRight ? checkOffset : -checkOffset), checkRect, 0, enemyLayer);
            for(int i = 0; i < hitEnemies.Length; i++)
            {
                // Get direction of knockback based on player direction 
                Vector2 dir = player.FacingRight ?
                    Vector2.Lerp(knockbackMinDir, knockbackMaxDir, Random.Range(0.0f, 1.0f)):
                    Vector2.Lerp(new Vector2(-knockbackMinDir.x, knockbackMinDir.y), new Vector2(-knockbackMaxDir.x, knockbackMaxDir.y), Random.Range(0.0f, 1.0f));

                hitEnemies[i].GetComponent<EnemyController>().GetHit(
                    knockBackMag,
                    dir // Random between two vectors 
                    );
            }
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
