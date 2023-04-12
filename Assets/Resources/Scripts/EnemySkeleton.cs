using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy {

    Rigidbody rigidbody;
    
    /// <summary>
    /// アニメーション
    /// </summary>
    EnemySkeletonAnimation enemySkeletonAnimation;

    [SerializeField]
    float life = 1f;

    /// <summary>
    /// 移動速度
    /// </summary>
    [SerializeField]
    float moveSpeed = 1f;

    [SerializeField]
    float deadTime = 5f;
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
    /// 反転するまでの待ち時間
    /// </summary>
    float returnWait = 1f;
    float returnNowTime = 0f;

    /// <summary>
    /// SE用
    /// </summary>
    SoundController soundController;

    /// <summary>
    /// 重力
    /// </summary>
    [SerializeField]
    float gravity = 1f;

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
    Vector3 enemyDamageBoxSize = new Vector3(1f,0.5f,1f);
    
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

    /// <summary>
    /// 右下左下が開いているとき反転するかどうか
    /// </summary>
    [SerializeField]
    bool spaceFindReturnFlag = false;
    private void Awake()
    {
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
        rigidbody = this.GetComponent<Rigidbody>();
        enemySkeletonAnimation = this.GetComponent<EnemySkeletonAnimation>();
    }
    void Start()
    {
        HitPoint = life;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + Vector3.up * enemyHeadHeight, enemyDamageBoxSize);
        Gizmos.DrawCube(transform.position + Vector3.up  * (enemyHeadHeight + enemyDamageBoxCastLength), enemyDamageBoxSize );


    }
    private void FixedUpdate()
    {
        // 生きてるときのみ実行
        if (Alive)
        {
            RaycastHit hit;
            // レイキャスト飛ばして接触したらNoJumpへ
            Physics.BoxCast(this.transform.position + Vector3.up * enemyHeadHeight, enemyDamageBoxSize / 2, transform.up, out hit, transform.rotation, enemyDamageBoxCastLength);

            // ボックスキャストに何かが当たった時
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

            if (spaceFindReturnFlag)
            {
                // レイキャスト飛ばして接触していなかったら反転する
                Physics.Raycast(this.transform.position + Vector3.up * 0.2f, -Vector3.up, out hit, 1f);
                //Debug.DrawRay(this.transform.position + Vector3.up * 0.2f, -Vector3.up, Color.blue, 1f);
                if (hit.collider != null)
                {
                    // レイキャスト飛ばして接触していなかったら反転する
                    Physics.Raycast(this.transform.position + Vector3.up * 0.2f + Vector3.right * enemyDamageBoxSize.x * 0.5f, -Vector3.up, out hit, 1f);
                    //Debug.DrawRay(this.transform.position + Vector3.up * 0.2f + Vector3.right * enemyDamageBoxSize.x * 0.5f, -Vector3.up, Color.red, 1f);
                    if (hit.collider == null)
                    {
                        // 自分の位置から右下方向に穴が開いていることが確認出来たら反転する
                        enemySkeletonAnimation.EnemyRight = false;
                        enemySkeletonAnimation.EnemyLeft = true;
                    }
                    else
                    {
                        // レイキャスト飛ばして接触していなかったら反転する
                        Physics.Raycast(this.transform.position + Vector3.up * 0.2f + -Vector3.right * enemyDamageBoxSize.x * 0.5f, -Vector3.up, out hit, 1f);
                        //Debug.DrawRay(this.transform.position + Vector3.up * 0.2f + -Vector3.right * enemyDamageBoxSize.x * 0.5f, -Vector3.up, Color.red,1f);
                        if (hit.collider == null)
                        {
                            // 自分の位置から左下方向に穴が開いていることが確認出来たら反転する
                            enemySkeletonAnimation.EnemyRight = true;
                            enemySkeletonAnimation.EnemyLeft = false;
                        }
                    }
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
        if (enemySkeletonAnimation.EnemyLeft)
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
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 hitPos = Vector3.zero;
        // 衝突した場所を取得
        foreach (ContactPoint point in collision.contacts)
        {
            // プレイヤーに踏まれたとき
            hitPos = point.point;
            // アタックオブジェクトに当たった時
            if (collision.transform.tag == "AttackObj")
            {
                HitAction(hitPos);
            }
        }



    }
    private void OnCollisionStay(Collision collision)
    {
        returnNowTime += Time.deltaTime;
        if (returnNowTime > returnWait)
        {
            Vector3 hitPos = Vector3.zero;
            // 衝突した場所を取得
            foreach (ContactPoint point in collision.contacts)
            {
                hitPos = point.point;
                rigidbody.velocity = Vector3.zero;

                if (-returnFlagHosei > hitPos.x - this.transform.position.x)
                {
                    enemySkeletonAnimation.EnemyRight = true;
                    enemySkeletonAnimation.EnemyLeft = false;
                }
                else if (returnFlagHosei < hitPos.x - this.transform.position.x)
                {
                    enemySkeletonAnimation.EnemyLeft = true;
                    enemySkeletonAnimation.EnemyRight = false;
                }
            }
            returnNowTime = 0f;
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
            enemySkeletonAnimation.HitAnimation();
        }
        else
        {
            // 死亡SE
            soundController.Monster();
            // コライダー外す
            //this.GetComponent<Collider>().enabled = false;
            // 物理挙動OFF
            //rigidbody.isKinematic = true;
            // タグを外す
            this.transform.tag = "Untagged";
            // プレイヤーに接触しないレイヤーに変更する
            this.transform.gameObject.layer = LayerMask.NameToLayer("PlayerNoCollision");
            // 動きを止める
            rigidbody.velocity = Vector3.zero;
            // アニメーション再生
            enemySkeletonAnimation.DeadAnimation();
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
