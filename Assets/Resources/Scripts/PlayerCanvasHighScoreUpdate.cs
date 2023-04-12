using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasHighScoreUpdate : MonoBehaviour {

    Text scoreText;

    GameObject[] player;
    PlayerStats playerStats;

    void Start()
    {
        scoreText = this.GetComponent<Text>();
        player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject scoreObj in player)
        {
            playerStats = scoreObj.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                scoreText.text = playerStats.HighScore.ToString();
                return;
            }
        }
    }

    void Update()
    {
        scoreText.text = playerStats.HighScore.ToString();
    }
}
