using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCactusAnimation : MonoBehaviour {

    Animator animatorGamaObject;
    bool enemyRight = true;

    Vector3 rightScale;
    Vector3 leftScale;
    private void Awake()
    {
        animatorGamaObject = this.GetComponent<Animator>();
        rightScale = this.transform.localScale;
        leftScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
    }

    void Update () {

        // 右が入力されたら
        if (false)
        {
            if (!enemyRight)
            {
                this.transform.localScale = rightScale;
            }
            WalkAnimation(true);
            enemyRight = true;
        }
        // 左が入力されたら
        else if (false)
        {
            if (enemyRight)
            {
                this.transform.localScale = leftScale;
            }
            WalkAnimation(true);
            enemyRight = false;
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
