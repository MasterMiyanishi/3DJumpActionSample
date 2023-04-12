using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// このクラスではプレイヤーのダメージ挙動を管理しています
/// </summary>
public class PlayerDamage : MonoBehaviour {
    PlayerStats playerStats;
    PlayerAnimation playerAnimation;

    float nowDamageTime = 0f;

    /// <summary>
    /// 無敵かどうかのフラグ
    /// </summary>
    [SerializeField]
    bool god = false;

    float footPosition = 0f;

    /// <summary>
    /// 攻撃が当たった時のエフェクト
    /// </summary>
    [SerializeField]
    GameObject hitEffect;

    /// <summary>
    /// 死亡時のエフェクト
    /// </summary>
    [SerializeField]
    GameObject deadEffect;

    Rigidbody rigidbody;

    /// <summary>
    /// SE用
    /// </summary>
    SoundController soundController;

    /// <summary>
    /// カメラを揺らす
    /// </summary>
    ObjectShaker objectShaker;

    public bool God
    {
        get
        {
            return god;
        }

        set
        {
            god = value;
        }
    }

    private void Awake()
    {
        playerStats = this.GetComponent<PlayerStats>();
        playerAnimation = this.GetComponent<PlayerAnimation>();
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();

        nowDamageTime = playerStats.GodTime;
        rigidbody = this.GetComponent<Rigidbody>();

        objectShaker = this.GetComponent<ObjectShaker>();
    }
    private void Update()
    {
        nowDamageTime += Time.deltaTime;

        if (playerStats.GodTime > nowDamageTime)
        {
            return;
        }
        else
        {
            God = false;
            playerAnimation.Flashing(God);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        EnemyColision(collision);
    }
    void EnemyColision(Collision collision)
    {
        if (God)
        {
            return;
        }
        if (!playerStats.PlayerAlive)
        {
            return;
        }
        // 衝突したのがEnemyタグ付きだったら
        if (collision.transform.tag == "Enemy")
        {

            // 体力を減らす
            playerStats.PlayerHitPoint--;

            // エフェクト再生
            hitEffect.transform.position = this.transform.position - transform.up * 0.5f;
            hitEffect.SetActive(true);

            // 生きているときはダメージアニメ
            if (playerStats.PlayerAlive)
            {
                // 画面を揺らす
                objectShaker.Shake(new Vector3( objectShaker.power / playerStats.PlayerHitPoint, objectShaker.power / playerStats.PlayerHitPoint, objectShaker.power));

                // ダメージSE
                soundController.Damage();

                playerAnimation.Damage();
                God = true;
                playerAnimation.Flashing(God);
            }
            // 死亡の時は
            else
            {
                // 画面を揺らす
                objectShaker.Shake(new Vector3( objectShaker.power , objectShaker.power , objectShaker.power));

                // 死亡SE
                soundController.Dead();

                // コライダー外す
                this.GetComponent<Collider>().enabled = false;
                // 物理挙動OFF
                //rigidbody.isKinematic = true;

                playerAnimation.Dead();
                // エフェクト再生
                deadEffect.transform.position = this.transform.position - transform.up * 0.5f;
                deadEffect.SetActive(true);
            }
            nowDamageTime = 0f;
        }
        // 衝突したのがDeadLineタグ付きだったら
        else if (collision.transform.tag == "DeadLine")
        {
            // 体力を0にする
            playerStats.PlayerHitPoint = 0;

            // 死亡SE
            soundController.Dead();

            // コライダー外す
            this.GetComponent<Collider>().enabled = false;
            // 物理挙動OFF
            //rigidbody.isKinematic = true;

            playerAnimation.Dead();
            // エフェクト再生
            deadEffect.transform.position = this.transform.position - transform.up * 0.5f;
            deadEffect.SetActive(true);
        }

    }
}
