using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplayManage : MonoBehaviour
{
    TMP_Text scoreText;
    GameSessionManage gameSession;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        gameSession = FindObjectOfType<GameSessionManage>();
    }

    void Update()
    {
        if (gameSession)
        {
            scoreText.SetText(gameSession.GetScore().ToString());
        }        
        //Debug.Log(gameSession.GetScore().ToString());
    }
}
