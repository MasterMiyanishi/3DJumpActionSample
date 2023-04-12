using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PursuitCameraQuarterView : MonoBehaviour
{
    /// <summary>
    /// 初期ポジション
    /// </summary>
    [SerializeField]
    Vector3 offsetPosition;

    /// <summary>
    /// 追跡するオブジェクト
    /// </summary>
    [SerializeField]
    GameObject pursuitObject;

    /// <summary>
    /// 画面を移動できる最小値
    /// </summary>
    [SerializeField]
    Vector3 minPosition = new Vector3(-5, -2, -10);
    /// <summary>
    /// 画面を移動できる最大値
    /// </summary>
    [SerializeField]
    Vector3 maxPosition = new Vector3(50, 20, -10);

    /// <summary>
    /// 新しいポジションの保存場所
    /// </summary>
    Vector3 newPosition;

    /// <summary>
    /// 更新頻度
    /// </summary>
    [SerializeField]
    float updateTime = 0.5f;

    float nowTime = 0f;

    /// <summary>
    /// iTweenを使うかどうか
    /// </summary>
    [SerializeField]
    bool iTweenEnabled = true;
    void Update()
    {
        nowTime += Time.deltaTime;

        // 追跡対象のポジションと初期位置を加算して保存
        newPosition = offsetPosition + pursuitObject.transform.position;

        // 範囲抑制
        newPosition = new Vector3(
                Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x),
                Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y),
                Mathf.Clamp(newPosition.z, minPosition.z, maxPosition.z));
        if (nowTime > updateTime)
        {

            // このクラスがついている対象物を移動
            if (iTweenEnabled)
            {
                //iTweenを使うことで滑らかに移動する
                iTween.MoveUpdate(this.gameObject, iTween.Hash("x", newPosition.x, "y", newPosition.y, "z", newPosition.z));
            }
            else
            {
                //this.transform.position = newPosition;

                this.transform.Translate(newPosition - this.transform.position);
            }
            nowTime = 0f;
        }

    }
}