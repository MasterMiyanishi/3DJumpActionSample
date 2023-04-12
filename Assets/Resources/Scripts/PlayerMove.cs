using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// このクラスではキャラクターの横方向に対する操作を実装しています
/// 学習項目は入力操作によるキャラクタの制動
/// </summary>
public class PlayerMove : MonoBehaviour {

    /// <summary>
    /// 加速倍率
    /// </summary>
    [SerializeField]
    float movePower = 1f;

    /// <summary>
    /// 加速の最大速度
    /// </summary>
    const float MAX_MOVE_POWER = 10f;

    /// <summary>
    /// プレイヤーのステータス
    /// </summary>
    PlayerStats playerStats;

    /// <summary>
    /// 加速用のワーク変数
    /// </summary>
    Vector3 newAction;

    private void Awake()
    {
        playerStats = this.GetComponent<PlayerStats>();
        if (playerStats.GhostMode)
        {
            GetComponent<Rigidbody>().useGravity = false;
        }
    }
    void Update ()
    {
        // 死亡時動かない
        if (!playerStats.PlayerAlive)
        {
            return;
        }

        // Horizontalの入力とパワーを掛け合わせてVector3に格納
        newAction = transform.right * Input.GetAxis("Horizontal") * movePower;

        // ゴーストの時上下方向にも移動する
        if (playerStats.GhostMode)
        {
            // Verticalの入力
            newAction += transform.up * Input.GetAxis("Vertical") ;
        }
        
        // パワーの最大値を超えたら最大値で補正する
        if (Mathf.Abs(newAction.x) > MAX_MOVE_POWER)
        {
            newAction = new Vector3(MAX_MOVE_POWER * Mathf.Sign(newAction.x), newAction.y, newAction.z);
        }

    }

    /// <summary>
    /// 決まった間隔で加速させるためFixedUpdateを使う
    /// </summary>
    private void FixedUpdate()
    {
        // 死亡時動かない
        if (!playerStats.PlayerAlive)
        {
            return;
        }
        // X方向に加速させる
        this.GetComponent<Rigidbody>().velocity = new Vector3(newAction.x, this.GetComponent<Rigidbody>().velocity.y+ newAction.y, this.GetComponent<Rigidbody>().velocity.z);
    }
}
