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

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        top.gameObject.SetActive(false);
        bottom.gameObject.SetActive(true);
        yourButton.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
