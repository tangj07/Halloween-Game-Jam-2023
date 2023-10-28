using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startButton : MonoBehaviour
{
    public bool start=false;

    public void onClick()
    {
        start = true;
        Debug.Log("sfdkjsf");
    }
    // Start is called before the first frame update
    void Start()
    {
        onClick();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
