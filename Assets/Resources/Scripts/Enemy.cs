using UnityEngine;

/// <summary>
/// 敵用の親クラス
/// すべての敵はこのクラスを継承する
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// 体力
    /// </summary>
    private float hitPoint = 1f;
    /// <summary>
    /// 生存フラグ
    /// </summary>
    private bool alive = true;

    public float HitPoint
    {
        get
        {
            return hitPoint;
        }

        set
        {
            hitPoint = value;
            // 体力が0以下になったら生存フラグをOFFにする
            if (hitPoint <= 0)
            {
                Alive = false;
            }
        }
    }
    public bool Alive
    {
        get
        {
            return alive;
        }

        set
        {
            alive = value;
        }
    }

}
