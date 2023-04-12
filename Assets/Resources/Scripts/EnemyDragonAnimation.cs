using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragonAnimation : MonoBehaviour {

    Animator animatorGamaObject;

    /// <summary>
    /// 右向きフラグ
    /// </summary>
    [SerializeField]
    bool enemyRight = false;
    /// <summary>
    /// 左向きフラグ
    /// </summary>
    [SerializeField]
    bool enemyLeft = false;

    Vector3 rightScale;
    Vector3 leftScale;

    public bool EnemyRight
    {
        get
        {
            return enemyRight;
        }

        set
        {
            enemyRight = value;
        }
    }

    public bool EnemyLeft
    {
        get
        {
            return enemyLeft;
        }

        set
        {
            enemyLeft = value;
        }
    }

    private void Awake()
    {
        animatorGamaObject = this.GetComponent<Animator>();
        rightScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z * -1);
        leftScale = this.transform.localScale;
    }

    void Update()
    {

        // 右が入力されたら
        if (EnemyRight)
        {
            this.transform.localScale = rightScale;
            WalkAnimation(true);
        }
        // 左が入力されたら
        else if (EnemyLeft)
        {
            this.transform.localScale = leftScale;
            WalkAnimation(true);
        }
        // 入力がない場合
        else
        {

            WalkAnimation(false);
        }
    }
    /// <summary>
    /// 歩行アニメーションの実行
    /// </summary>
    public void WalkAnimation(bool flag)
    {
        animatorGamaObject.SetBool("Walk", flag);
    }
    /// <summary>
    /// ヒットアニメーションの実行
    /// </summary>
    public void HitAnimation()
    {
        animatorGamaObject.SetTrigger("Hit");
    }
    /// <summary>
    /// 死亡アニメーションの実行
    /// </summary>
    public void DeadAnimation()
    {
        animatorGamaObject.SetTrigger("Dead");
    }
}
