using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject botCam;
    [SerializeField] GameObject topCam;

    private bool inGame = false;

    // Update is called once per frame
    void Update()
    {

        if(inGame)
        {
            // Return to Menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                botCam.SetActive(true);
                topCam.SetActive(false);
                inGame = false;
            }
        }
        else
        {
            // Begin Game 
            if (Input.GetMouseButtonDown(0))
            {
                botCam.SetActive(false);
                topCam.SetActive(true);
                inGame = true;
            }
        }
    }
}
