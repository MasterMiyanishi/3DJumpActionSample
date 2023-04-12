using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour {

    [SerializeField]
    string nextStageName = "Stage1";

    SoundController soundController;
    private void Awake()
    {
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
    }

    void Update () {

        // デリートキーが押されたらハイスコア削除
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            soundController.Cancel();
            this.GetComponent<PlayerStats>().DelHighScore();

        }
        // 何か押されたらステージ移動
        else if (Input.anyKeyDown)
        {
            soundController.Select();
            this.GetComponent<NextScene>().NextSceneMove(nextStageName);
        }
	}
}
