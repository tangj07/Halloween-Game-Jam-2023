using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimtorEvents : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Punch puncher;
    [SerializeField] GameObject punchImpactFX;
    [SerializeField] Vector3 punchImpactFXPos;
    public void ChangePunchState(int state)
    {
        bool change = state % 2 == 1;
        animator.SetBool("isPunching", change);
    }

    public void SetHurtState(int state)
    {
        bool change = state % 2 == 1;
        animator.SetBool("isHurt", change);
    }

    /// <summary>
    /// Blend from 0-1
    /// </summary>
    /// <param name="value"></param>
    public void ChangePunchTypeState(float value)
    {
        value = Mathf.Clamp01(value);
        animator.SetFloat("Blend", value);
    }

    public void CallPunch()
    {
        puncher.PunchEvent();
    }

    public void SpawnPunchFX()
    {
        bool isTrue = puncher.GetComponent<Player>().FacingRight;
        GameObject temp = Instantiate(punchImpactFX, this.transform.position + (isTrue ? punchImpactFXPos : new Vector3(-punchImpactFXPos.x, punchImpactFXPos.y, 0)), Quaternion.identity);
        if(!isTrue)
        {
            temp.transform.right = Vector3.left;
        }

        temp.transform.parent = this.transform.parent;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position + punchImpactFXPos, 0.1f);
    }

}
