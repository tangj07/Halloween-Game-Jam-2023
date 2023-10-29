using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.PackageManager;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject DeathDisplay;
    [SerializeField] TextMeshPro finalTimeMesh;
    [SerializeField] AnimtorEvents events;
    [Space]
    [SerializeField] GameObject playItems;

    [Header("To Start game")]
    [SerializeField] Transform targetCam;
    [SerializeField] Transform mainCam;
    private float timer;
    private GameObject currentPlayItems;

    private GameStates gameState;
    private enum GameStates
    {
        Menu,
        Tryplay,
        Playing,
        Dead
    }

    public void StartGame()
    {
        gameState = GameStates.Playing;
        timer = 0;

        // Just in case 
        DeathDisplay.SetActive(false);

        if(currentPlayItems != null)
        {
            Destroy(currentPlayItems);
        }

        currentPlayItems = Instantiate(playItems, Vector3.zero, Quaternion.identity);
    }

    public void EndGame()
    {
        gameState = GameStates.Dead;


        finalTimeMesh.text = "You Survived for " + (int)timer + "[s]";
        DeathDisplay.SetActive(true);

        events.ChangePunchTypeState(GameObject.FindObjectOfType<Punch>().GetState);
    }

    private void Start()
    {
        gameState = GameStates.Tryplay;
    }

    private void Update()
    {
        if(gameState == GameStates.Playing)
        {
            timer += Time.unscaledDeltaTime;
        }

        if(gameState == GameStates.Tryplay)
        {
            TryStartGame();
        }
        else if(gameState == GameStates.Dead)
        {
            if(Input.GetMouseButtonDown(0))
            {
                TryStartGame();
            }
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void TryStartGame()
    {
        if (Vector3.Distance(mainCam.position, targetCam.position) <= 0.1f)
        {
            StartGame();
        }
    }
}
