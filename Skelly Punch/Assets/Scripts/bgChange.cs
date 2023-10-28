using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgChange : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> otherBg;
    public List<bool> diffBg; 
    void ChangeBg(Sprite bgChange)
    {
        spriteRenderer.sprite = bgChange;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < otherBg.Count; i++)
        {
            if (diffBg[i])
            {
                ChangeBg(otherBg[i]);
                diffBg[i] = false;
            }
        } 
    }
}
