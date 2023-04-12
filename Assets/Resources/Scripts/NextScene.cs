using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    GameObject[] player;
    PlayerStats playerStats;

    string TITLE_SCENE_NAME = "Title";
    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject scoreObj in player)
        {
            playerStats = scoreObj.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                return;
            }
        }
    }
    /// <summary>
    /// 新しいシーンに飛ぶ
    /// </summary>
    /// <param name="nextSceneName">飛ぶシーンの名前</param>
    public void NextSceneMove(string nextSceneName)
    {
        // スコアを一時的に保存
        playerStats.SaveScore();
        // ハイスコアを保存
        playerStats.SaveHighScore();

        // シーンを追加
        SceneManager.LoadSceneAsync(nextSceneName);

        // 追加シーンを変数へ
        Scene nextScnen = SceneManager.GetSceneByName(nextSceneName);

        // 現在のシーン名を取得
        string thisSceneName = SceneManager.GetActiveScene().name;

        // 追加したシーンが無ければ自分を再度読み込む
        if (!nextScnen.IsValid())
        {
            SceneManager.LoadScene(thisSceneName);
        }
    }
    /// <summary>
    /// タイトル画面に戻る
    /// </summary>
    public void TitleSceneMove()
    {
        // スコアを削除
        playerStats.DelScore();
        // ハイスコアを保存
        playerStats.SaveHighScore();
        SceneManager.LoadSceneAsync(TITLE_SCENE_NAME);
    }
    /// <summary>
    /// ゲームをリトライ
    /// </summary>
    public void SceneReMove()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
