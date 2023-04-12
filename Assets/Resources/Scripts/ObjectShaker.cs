using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// iTweenを使って対象を揺らすスクリプト
/// </summary>
public class ObjectShaker : MonoBehaviour {

    public float shakePointX = 0.1f;
    public float shakePointY = 0.1f;
    public float shakeTime = 0.1f;

    public GameObject shakeObj;

    public float power = 10;

    /// <summary>
    /// 指定されたオブジェクトを揺らす
    /// </summary>
    public void Shake()
    {
        Shake(power);
    }
    /// <summary>
    /// 指定されたオブジェクトを揺らす
    /// </summary>
    /// <param name="power">揺らす強さを指定する</param>
    public void Shake(float power)
    {
        Vector3 allSetPower = new Vector3(power, power, power);
        Shake(allSetPower);
    }
    /// <summary>
    /// 指定されたオブジェクトを揺らす
    /// </summary>
    /// <param name="power">揺らす強さを指定する(xとyと揺らす時間を別々に)</param>
    public void Shake(Vector3 power)
    {
        // 揺らすオブジェクトがない場合何もしない
        if (shakeObj == null)
        {
            return;
        }

        iTween.ShakeRotation(shakeObj, iTween.Hash("x", shakePointX * power.x, "y", shakePointY * power.y, "time", shakeTime * power.z));
    }
}
