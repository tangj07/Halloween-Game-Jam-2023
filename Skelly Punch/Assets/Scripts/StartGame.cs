using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public CinemachineVirtualCamera start;
    public CinemachineVirtualCamera game;
    public startButton instance;
    // Start is called before the first frame update
    void Start()
    {
        instance.GetComponent<startButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (instance.start)
        {
            start.gameObject.SetActive(false);
            game.gameObject.SetActive(true);
        }

    }
}
