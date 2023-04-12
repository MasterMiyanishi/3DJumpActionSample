using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 親オブジェクトの座標を基にローカル座標を固定するクラス
/// </summary>
public class localPsitionSet : MonoBehaviour {

    /// <summary>
    /// 親のトランスフォームポジション
    /// </summary>
    Vector3 parnetPosition;

    /// <summary>
    /// オフセット
    /// </summary>
    [SerializeField]
    Vector3 offsetPosition;

    void Update () {

        this.transform.position = parnetPosition;

    }
    private void OnEnable()
    {
        parnetPosition = this.transform.parent.position + offsetPosition;
    }
}
