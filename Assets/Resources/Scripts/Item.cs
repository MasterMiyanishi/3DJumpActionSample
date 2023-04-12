using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム用のクラス
/// </summary>
public class Item : MonoBehaviour {

    /// <summary>
    /// プレイヤーのタグ名
    /// </summary>
    string PLAYER_TAG_NAME = "Player";

    /// <summary>
    /// 追加するスコア値
    /// </summary>
    [SerializeField]
    int addScore = 0;

    /// <summary>
    /// アイテムの一意の文字列
    /// </summary>
    string uuid = "";

    public int AddScore
    {
        get
        {
            return addScore;
        }

        set
        {
            addScore = value;
        }
    }

    /// <summary>
    /// 追加するHP値
    /// </summary>
    [SerializeField]
    float addHitPoint = 0f;
    public float AddHitPoint
    {
        get
        {
            return addHitPoint;
        }

        set
        {
            addHitPoint = value;
        }
    }

    /// <summary>
    /// UUID
    /// </summary>
    public string Uuid
    {
        get
        {
            return uuid;
        }

        set
        {
            uuid = value;
        }
    }

    private void Awake()
    {
        Uuid = System.Guid.NewGuid().ToString();
    }
    /// <summary>
    /// 触れたときの処理
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーが触れたら見えなく触れなくする
        if (collision.gameObject.tag == PLAYER_TAG_NAME)
        {
            this.GetComponent<Collider>().enabled = false;
            this.GetComponent<MeshRenderer>().enabled = false;

            // 子オブジェクトを検索して非表示化
            foreach (Transform transform in gameObject.transform)
            {
                // Transformからゲームオブジェクト取得・削除
                transform.gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// オントリガー用の処理
    /// Righitbody外してコライダーをトリガーに変更することで
    /// 空中固定式に変更できるはず...
    /// </summary>
    /// <param name="other"></param>
    //private void OnTriggerEnter(Collider other)
    //{

    //    // プレイヤーが触れたら見えなく触れなくする
    //    if (other.gameObject.tag == PLAYER_TAG_NAME)
    //    {
    //        this.GetComponent<Collider>().enabled = false;
    //        this.GetComponent<MeshRenderer>().enabled = false;
    //    }
    //}

}
