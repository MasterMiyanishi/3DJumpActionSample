using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : Enemy
{

    Rigidbody rigidbody;

    /// <summary>
    /// アニメーション
    /// </summary>
    EnemyDragonAnimation enemyDragonAnimation;

    [SerializeField]
    float life = 1f;

    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    float moveSpeed = 1f;

    [SerializeField]
    float deadTime = 2f;
    float nowTime = 0f;

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

    /// <summary>
    /// SE用
    /// </summary>
    SoundController soundController;

    /// <summary>
    /// 重力
    /// </summary>
    [SerializeField]
    float gravity = 0f;

    /// <summary>
    /// ものにぶつかっても反転しない
    /// </summary>
    [SerializeField]
    bool rotationNoChenge = false;
    /// <summary>
    /// 移動可能フラグ
    /// </summary>
    [SerializeField]
    bool move = false;

    /// <summary>
    /// 攻撃される高さ
    /// </summary>
    [SerializeField]
    float enemyHeadHeight = 1f;

    /// <summary>
    /// 攻撃される範囲
    /// </summary>
    [SerializeField]
    Vector3 enemyDamageBoxSize = new Vector3(1f, 0.5f, 1f);

    /// <summary>
    /// 攻撃される範囲の長さ
    /// </summary>
    [SerializeField]
    float enemyDamageBoxCastLength = 0.3f;


    /// <summary>
    /// 死亡したあとデストロイするかどうか
    /// </summary>
    [SerializeField]
    bool deadDestroyFlag = false;

    /// <summary>
    /// ぶつかったら反転するx座標
    /// </summary>
    [SerializeField]
    float returnFlagHosei = 0.2f;

    private void Awake()
    {
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
        rigidbody = this.GetComponent<Rigidbody>();
        enemyDragonAnimation = this.GetComponent<EnemyDragonAnimation>();
    }
    void Start()
    {
        HitPoint = life;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + Vector3.up * enemyHeadHeight, enemyDamageBoxSize);
        Gizmos.DrawCube(transform.position + Vector3.up * (enemyHeadHeight + enemyDamageBoxCastLength), enemyDamageBoxSize);
    }
    private void FixedUpdate()
    {
        // 生きてるときのみ実行
        if (Alive)
        {
            RaycastHit hit;
            // レイキャスト飛ばして接触したらNoJumpへ
            Physics.BoxCast(this.transform.position + Vector3.up * enemyHeadHeight, enemyDamageBoxSize / 2, transform.up, out hit, transform.rotation, enemyDamageBoxCastLength);

            // ボックスキャストに何かが当たった時かつ
            // ジャンプが終了したら
            if (hit.collider != null)
            {
                // 敵を踏みつけたときバウンドする
                if (hit.transform.tag == "Player" && !hit.transform.gameObject.GetComponent<PlayerDamage>().God)
                {
                    // エフェクト表示
                    hit.transform.gameObject.GetComponent<PlayerJump>().ShowJumpEffect();
                    hit.transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(
                        hit.transform.gameObject.GetComponent<Rigidbody>().velocity.x,
                        hit.transform.gameObject.GetComponent<PlayerJump>().BoundPower,
                        hit.transform.gameObject.GetComponent<Rigidbody>().velocity.z);

                    HitAction(this.transform.position + Vector3.up * enemyHeadHeight);
                }

            }
        }

        // 死亡後アクティブ解除するための処理
        if (!Alive)
        {
            nowTime += Time.deltaTime;
        }

        if (nowTime > deadTime)
        {
            // デストロイするかどうか
            // しない場合は非表示にする
            if (deadDestroyFlag)
            {
                Destroy(this.gameObject);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }

        // 移動フラグがOFFの時は動かない
        if (!move)
        {
            return;
        }

        // 反転処理
        float returnValue = 1f;
        if (enemyDragonAnimation.EnemyLeft)
        {
            returnValue *= -1f;
        }
        if (Alive)
        {
            rigidbody.velocity = new Vector3(moveSpeed * returnValue, rigidbody.velocity.y, rigidbody.velocity.z);
        }

        // 重力落下処理
        rigidbody.velocity += -transform.up * gravity;
    }
    private void OnCollisionStay(Collision collision)
    {
        Vector3 hitPos = Vector3.zero;

        // 衝突した場所を取得
        foreach (ContactPoint point in collision.contacts)
        {
            hitPos = point.point;
            // アタックオブジェクトに当たった時
            if (collision.transform.tag == "AttackObj")
            {
                HitAction(hitPos);
            }
        }

        // ぶつかると反転する
        if (!rotationNoChenge)
        {
            // 衝突した場所を取得
            foreach (ContactPoint point in collision.contacts)
            {
                hitPos = point.point;
                rigidbody.velocity = Vector3.zero;

                if (-returnFlagHosei > hitPos.x - this.transform.position.x)
                {
                    enemyDragonAnimation.EnemyRight = true;
                    enemyDragonAnimation.EnemyLeft = false;
                }
                else if (returnFlagHosei < hitPos.x - this.transform.position.x)
                {
                    enemyDragonAnimation.EnemyLeft = true;
                    enemyDragonAnimation.EnemyRight = false;
                }
            }
        }

    }
    private void HitAction(Vector3 hitPos)
    {
        HitPoint--;
        // エフェクト再生
        hitEffect.transform.position = hitPos;
        hitEffect.SetActive(true);
        if (Alive)
        {
            // ダメージSE
            soundController.Damage();
            // アニメーション再生
            enemyDragonAnimation.HitAnimation();
        }
        else
        {
            // 死亡SE
            soundController.Monster();
            // コライダー外す
            //foreach (BoxCollider collider in this.GetComponentsInChildren<BoxCollider>())
            //{
            //    //collider.enabled = false;
            //}
            // 物理挙動OFF
            //rigidbody.isKinematic = true;
            rigidbody.useGravity = true;
            // タグを外す
            this.transform.tag = "Untagged";
            // プレイヤーに接触しないレイヤーに変更する
            this.transform.gameObject.layer = LayerMask.NameToLayer("PlayerNoCollision");
            // 動きを止める
            rigidbody.velocity = Vector3.zero;
            // アニメーション再生
            enemyDragonAnimation.DeadAnimation();
            // エフェクト再生
            deadEffect.transform.position = this.transform.position;
            deadEffect.SetActive(true);
        }
    }
    /// <summary>
    /// プレイヤーがトリガーコライダーに入ったら動き出す
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            move = true;
        }
    }
}
