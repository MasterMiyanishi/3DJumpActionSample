using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvasHitPointUpdate : MonoBehaviour {

    Slider hitPointSlider;

    GameObject player;
    PlayerStats playerStats;

	void Start () {
        hitPointSlider = this.GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        hitPointSlider.maxValue = playerStats.PlayerMaxHitPoint;
        hitPointSlider.value = playerStats.PlayerHitPoint;
    }

    void Update () {

        hitPointSlider.maxValue = playerStats.PlayerMaxHitPoint;
        hitPointSlider.value = playerStats.PlayerHitPoint;
    }
}
