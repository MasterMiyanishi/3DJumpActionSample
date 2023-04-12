using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasSoreUpdate : MonoBehaviour {

    Text scoreText;

    GameObject[] player;
    PlayerStats playerStats;

    void Start () {
        scoreText = this.GetComponent<Text>();
        player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject scoreObj in player)
        {
            playerStats = scoreObj.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                scoreText.text = playerStats.Score.ToString();
                return;
            }
        }
    }
	
	void Update () {

        scoreText.text = playerStats.Score.ToString();
    }
}
