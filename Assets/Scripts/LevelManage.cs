using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManage : MonoBehaviour
{
    GameSessionManage gameSession;
    [SerializeField] float delayInSeconds = 4f;

    public void LoadGameScene()
    {
        // Destroy previous session
        gameSession = FindObjectOfType<GameSessionManage>();
        if (gameSession)
        {
            gameSession.ResetGame();
        }

        SceneManager.LoadScene("GameScene");
    }

    public void LoadMenuScene() { StartCoroutine(WaitAndLoadMenu()); }

    IEnumerator WaitAndLoadMenu()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("MenuScene");
    }
}
