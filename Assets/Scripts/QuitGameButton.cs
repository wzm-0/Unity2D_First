using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuitGameButton : MonoBehaviour
{
    public Button quitButton;

    private void Start()
    {
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(QuitGame);
        }
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameCoroutine());
    }

    IEnumerator QuitGameCoroutine()
    {
        // 释放资源
        yield return Resources.UnloadUnusedAssets();

        // 保存数据等操作
        // SaveGameData();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

     private void SaveGameData()
    {
        // 保存游戏数据
        // 示例：使用 PlayerPrefs 保存数据
        PlayerPrefs.SetString("SaveData", "SomeData");
        PlayerPrefs.Save();
    }
}