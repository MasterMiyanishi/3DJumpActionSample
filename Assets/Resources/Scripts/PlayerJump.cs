using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// このクラスではキャラクターのジャンプ操作を実装しています
/// 学習項目としてはenumによるステート管理と
/// RayCastを使った衝突判定です
/// </summary>
public class PlayerJump : MonoBehaviour {
    [SerializeField]
    bool autoJump = false;

    /// <summary>
    /// ジャンプの状態
    /// </summary>
    public enum JumpState
    {
        NoJump,
        JumpStart,
        JumpStay,
        JumpEnd,
    }
    /// <summary>
    /// 現在のジャンプ状態
    /// </summary>
    [SerializeField]
    JumpState jumpState = JumpState.NoJump;

    /// <summary>
    /// 現在の加速度
    /// </summary>
    Vector3 newAction;

    /// <summary>
    /// ジャンプの強さ
    /// </summary>
    [SerializeField]
    float jumpPower = 1f;

    /// <summary>
    /// ジャンプの最大実行回数
    /// </summary>
    const int MAX_JUMP_COUNT = 16;

    int jumpCount = 0;

    float jumpTime = 0f;

    /// <summary>
    /// ジャンプの最大時間
    /// </summary>
    const float JUMP_MAX_TIME = 0.2f;
    
    /// <summary>
    /// 重力
    /// </summary>
    [SerializeField]
    float gravity = 0.1f;

    /// <summary>
    /// アニメーション用
    /// </summary>
    PlayerAnimation playerAnimation;

    /// <summary>
    /// プレイヤーのステータス
    /// </summary>
    PlayerStats playerStats;

    /// <summary>
    /// 落下時のエフェクト
    /// </summary>
    [SerializeField]
    GameObject stompEffect;

    /// <summary>
    /// ジャンプ時のエフェクト
    /// </summary>
    [SerializeField]
    GameObject jumpEffect;

    /// <summary>
    /// SE用
    /// </summary>
    SoundController soundController;

    /// <summary>
    /// 落下エフェクトを出す衝撃の強さ
    /// </summary>
    [SerializeField]
    float stompEffectMagnitude = 5f;
    private Rigidbody rigidbody;

    /// <summary>
    /// 敵を踏んだときバウンドする力
    /// </summary>
    [SerializeField]
    float boundPower =2f;

    public float BoundPower
    {
        get
        {
            return boundPower;
        }

        set
        {
            boundPower = value;
        }
    }

    private void Awake()
    {
        playerAnimation = this.GetComponent<PlayerAnimation>();
        playerStats = this.GetComponent<PlayerStats>();
        rigidbody = this.GetComponent<Rigidbody>();
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
    }
    private void Update()
    {
        // 死亡時動かない
        if (!playerStats.PlayerAlive)
        {
            return;
        }

        RaycastHit hit;
        // レイキャスト飛ばして接触したらNoJumpへ
        Physics.BoxCast(this.transform.position - transform.up*0.5f, new Vector3(0.4f,0.05f,0.4f), -transform.up, out hit, transform.rotation,0.5f);
        
        // ボックスキャストに何かが当たった時かつ
        // ジャンプが終了したら
        if (hit.collider != null && jumpState == JumpState.JumpEnd)
        {
            // ジャンプアニメーションの終了
            playerAnimation.Stomp();

            // 着地音
            soundController.Stomp();

            jumpState = JumpState.NoJump;

            // 落下時のエフェクト
            if (rigidbody.velocity.y < -stompEffectMagnitude)
            {
                stompEffect.transform.position = this.transform.position - transform.up * 0.75f;
                stompEffect.SetActive(true);
            }

        }
        else if (hit.collider == null && jumpState == JumpState.NoJump)
        {
            jumpState = JumpState.JumpEnd;
        }


        switch (jumpState)
        {
            case JumpState.NoJump:
                // ジャンプボタンが押されたら
                if (Input.GetButtonDown("Jump") || autoJump)
                {
                    jumpState = JumpState.JumpStart;
                }


                break;
            case JumpState.JumpStart:

                // ジャンプアニメーションの開始
                playerAnimation.JumpAnimation();

                // ジャンプ音
                soundController.Jump();

                // ジャンプエフェクト
                ShowJumpEffect();

                jumpState = JumpState.JumpStay;

                break;
            case JumpState.JumpStay:

                // ジャンプボタンはなしたらジャンプ終了
                if (Input.GetButtonUp("Jump"))
                {
                    jumpState = JumpState.JumpEnd;
                }


                break;
            case JumpState.JumpEnd:

                break;
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
        switch (jumpState)
        {
            case JumpState.JumpStart:
                newAction = rigidbody.velocity;
                rigidbody.velocity = new Vector3(0f, newAction.y, newAction.z);

                break;
            case JumpState.JumpStay:

                // ジャンプの時間計測
                jumpTime += Time.deltaTime;
                // ジャンプ時間が最大値を超えたらジャンプ終了
                if (jumpTime > JUMP_MAX_TIME)
                {
                    jumpState = JumpState.JumpEnd;
                }

                // ジャンプの高さが最大値を超えたらジャンプ終了
                if (jumpCount >= MAX_JUMP_COUNT)
                {
                    jumpState = JumpState.JumpEnd;
                }

                jumpCount++;
                newAction = rigidbody.velocity;
                rigidbody.velocity = newAction + transform.up * jumpPower;
                break;
            case JumpState.JumpEnd:

                // ジャンプの時間をリセット
                jumpTime = 0f;

                // ジャンプカウントのリセット
                jumpCount = 0;

                // ゴーストの時は重力落下処理をしない
                if (!playerStats.GhostMode)
                {
                    // 重力落下処理
                    rigidbody.velocity += -transform.up * gravity;
                }

                break;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        // 落下中の場合
        if (rigidbody.velocity.y < 0)
        {

            jumpState = JumpState.JumpEnd;
        }
    }
    public void ShowJumpEffect()
    {

        if (jumpEffect.GetComponent<ParticleSystem>().isPlaying)
        {
            jumpEffect.SetActive(false);
        }
        jumpEffect.SetActive(true);
    }
}
