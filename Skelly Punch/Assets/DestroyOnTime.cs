using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    [SerializeField] float timeToDie;
    // Start is called before the first frame update
    void Start()
    {
        if (timeToDie < 0)
            return;

        Destroy(this.gameObject, timeToDie);
    }
}
