using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameController : MonoBehaviour
{
    public GameObject startButton; // 开始按钮
    public GameObject gameObjectsToActivateOnStart; // 游戏开始时激活的对象

    private bool gameStarted = false;

    private void Start()
    {
        // 初始化时隐藏游戏对象
        if (gameObjectsToActivateOnStart != null)
        {
            gameObjectsToActivateOnStart.SetActive(false);
        }

        // 设置按钮点击事件
        if (startButton != null)
        {
            startButton.GetComponent<Button>().onClick.AddListener(StartGame);
        }
    }

    public void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            startButton.SetActive(false); // 隐藏开始按钮
            gameObjectsToActivateOnStart.SetActive(true); // 显示游戏对象
        }
    }
}
