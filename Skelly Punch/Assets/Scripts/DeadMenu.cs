using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
    [SerializeField] GameObject visual;
    private bool menuOn = false;

    private void Start()
    {
        menuOn = false;
    }

    public void SetMenuActive()
    {
        menuOn = true;
    }

    private void Update()
    {
        if (!menuOn)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                visual.SetActive(true);
                menuOn = true;
            }
            
            return;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // Go to main menu
        }
        else if(Input.GetMouseButtonDown(0))
        {
            // Just reload scene 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
