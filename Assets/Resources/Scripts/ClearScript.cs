using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScript : MonoBehaviour {

    NextScene nextScene;

    /// <summary>
    /// クリア後に飛ぶシーン名
    /// </summary>
    [SerializeField]
    string nextSceneName = "";

    /// <summary>
    /// シーン切り替えしたかどうかのフラグ
    /// </summary>
    bool sceneChengeFlag = false;

    /// <summary>
    /// 次のシーンに切り替わる前の停止時間
    /// </summary>
    [SerializeField]
    float nextSceneMoveWait = 1f;
    private void Start()
    {
        nextScene = this.GetComponent<NextScene>();

    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーが触れたら次のシーンに移動
        if (other.gameObject.tag == "Player" && !sceneChengeFlag)
        {
            other.gameObject.GetComponent<PlayerStats>().PlayerAlive = false;

            sceneChengeFlag = true;
            StartCoroutine(ClearEvent());
        }
    }
    IEnumerator ClearEvent()
    {
        this.transform.Find("EndGameCanvas").GetComponent<Animator>().SetTrigger("StartFade");

        yield return new WaitForSeconds(nextSceneMoveWait);

        nextScene.NextSceneMove(nextSceneName);

        yield break;

    }
}
