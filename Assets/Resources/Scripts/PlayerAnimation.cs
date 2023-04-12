using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// このクラスではキャラクターのアニメーションを実装しています
/// 学習項目としてはアニメーションの実行です
/// </summary>
public class PlayerAnimation : MonoBehaviour {
    [SerializeField]
    GameObject animationGamaObject;
    Animator animatorGamaObject;
    bool playerRight = true;

    Vector3 rightScale;
    Vector3 leftScale;

    /// <summary>
    /// プレイヤーのステータス
    /// </summary>
    PlayerStats playerStats;

    private void Awake()
    {
        animatorGamaObject = animationGamaObject.GetComponent<Animator>();
        playerStats = this.GetComponent<PlayerStats>();
        rightScale = this.transform.localScale;
        leftScale = new Vector3(this.transform.localScale.x*-1, this.transform.localScale.y, this.transform.localScale.z);
    }
    private void Update()
    {
        // 死亡時動かない
        if (!playerStats.PlayerAlive)
        {
            return;
        }
        // 右が入力されたら
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            if (!playerRight)
            {
                this.transform.localScale = rightScale;
            }
            animatorGamaObject.SetBool("Walk",true);
            playerRight = true;
        }
        // 左が入力されたら
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            if (playerRight)
            {
                this.transform.localScale = leftScale;
            }
            animatorGamaObject.SetBool("Walk", true);
            playerRight = false;
        }
        // 入力がない場合
        else
        {

            animatorGamaObject.SetBool("Walk", false);
        }

    }
    /// <summary>
    /// ジャンプアニメーションの実行
    /// </summary>
    public void JumpAnimation()
    {
        animatorGamaObject.SetTrigger("Jump");
    }
    /// <summary>
    /// 着地アニメーションの実行
    /// </summary>
    public void Stomp()
    {
        animatorGamaObject.SetTrigger("Stomp");
    }
    /// <summary>
    /// ダメージを受けるアニメーションの実行
    /// </summary>
    public void Damage()
    {
        animatorGamaObject.SetTrigger("Damage");
    }
    /// <summary>
    /// 死亡アニメーションの実行
    /// </summary>
    public void Dead()
    {
        animatorGamaObject.SetTrigger("Dead");
    }
    public void Flashing(bool flag)
    {
        animatorGamaObject.SetBool("Flashing", flag);
    }
}
