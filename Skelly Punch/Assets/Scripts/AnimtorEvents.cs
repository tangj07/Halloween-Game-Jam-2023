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

    public void CallPunch()
    {
        puncher.PunchEvent();
    }

}
