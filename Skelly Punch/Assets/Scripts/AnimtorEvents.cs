using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimtorEvents : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Punch puncher;
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

}
