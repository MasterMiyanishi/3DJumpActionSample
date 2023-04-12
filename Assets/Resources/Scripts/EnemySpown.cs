using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpown : MonoBehaviour {
    /// <summary>
    /// スポーンするオブジェクト
    /// </summary>
    [SerializeField]
    GameObject spownEnemy;

    /// <summary>
    /// スポーンするまでのインターバル
    /// </summary>
    [SerializeField]
    float spownInterval = 5f;
    float nowTime = 0f;

    [SerializeField]
    Vector3 rotationOffset;
    private void Update()
    {
        nowTime += Time.deltaTime;
        // インターバルを超えたらスポーン
        if (nowTime > spownInterval)
        {
            Instantiate(spownEnemy,transform.position,Quaternion.Euler(rotationOffset));
            nowTime = 0f;
        }
    }

}
