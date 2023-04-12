using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チェストを開けたときのアニメーションと
/// アイテムをポップする処理
/// </summary>
public class OpenChest : MonoBehaviour {
    /// <summary>
    /// ポップアップするアイテム
    /// </summary>
    [SerializeField]
    GameObject[] popUpItems;

    /// <summary>
    /// ポップアップを開始する前の待ち時間
    /// </summary>
    float popUpStartWait = 1f;
    /// <summary>
    /// ポップアップを繰り返す待ち時間
    /// </summary>
    float popUpLoopWait = 0.2f;

    /// <summary>
    /// 開いているかどうかのフラグ
    /// </summary>
    bool open = false;

    /// <summary>
    /// SE用
    /// </summary>
    SoundController soundController;

    /// <summary>
    /// 左出力のパワー(-にすると逆向きになる)
    /// </summary>
    [SerializeField]
    float leftPower = 100f;

    /// <summary>
    /// 上出力のパワー(-にすると逆向きになる)
    /// </summary>
    [SerializeField]
    float upPower = 100f;

    /// <summary>
    /// チェストのエフェクト
    /// </summary>
    [SerializeField]
    GameObject chestEffect;
    private void Awake()
    {

        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!open)
        {
            open = true;

            // SE
            soundController.Door();

            // アニメーション実行
            GetComponent<Animator>().SetBool("open", true);

            // アイテム生成処理
            StartCoroutine(ItemPopUpEvent());

            chestEffect.SetActive(false);
        }
    }
    /// <summary>
    /// アイテムを出現させる処理
    /// </summary>
    /// <returns></returns>
    IEnumerator ItemPopUpEvent()
    {
        yield return new WaitForSeconds(popUpStartWait);

        // 登録されているアイテム分ループする
        foreach (GameObject popUpItem in popUpItems)
        {
            // データが登録されていないとき
            if (popUpItem == null)
            {
                yield break;
            }
            // 生成
            GameObject popUpItemObject = Instantiate(popUpItem);
            popUpItemObject.transform.position = this.transform.position + Vector3.up + -Vector3.right * Mathf.Sign(leftPower);

            // Rigidbodyがあるときだけ実行する処理
            if (popUpItemObject.GetComponent<Rigidbody>() != null)
            {
                // 左上に発射
                popUpItemObject.GetComponent<Rigidbody>().AddForce(-Vector3.right * leftPower + Vector3.up * upPower);
            }

            // 次回生成までの待ち
            yield return new WaitForSeconds(popUpLoopWait);
        }
        yield break;
    }
}
