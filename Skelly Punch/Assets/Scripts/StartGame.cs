using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;

public class StartGame : MonoBehaviour
{
    public CinemachineVirtualCamera top;
    public CinemachineVirtualCamera bottom;
    public Button yourButton;
    public bool startEverythingElse = false;
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        top.gameObject.SetActive(true);
        bottom.gameObject.SetActive(false);
        yourButton.gameObject.SetActive(false);
        startEverythingElse = true;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
