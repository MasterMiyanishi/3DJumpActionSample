using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFallingSound : MonoBehaviour {


    /// <summary>
    /// SE用
    /// </summary>
    SoundController soundController;

    /// <summary>
    /// 音を鳴らす衝撃の強さ
    /// </summary>
    [SerializeField]
    float soundOutMagnitude = 1f;

    Rigidbody rigidbody;

    /// <summary>
    /// 重力
    /// </summary>
    [SerializeField]
    float gravity = 1f;
    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        soundController = GameObject.Find("SoundController").GetComponent<SoundController>();
    }
    private void FixedUpdate()
    {

        // 重力落下処理
        rigidbody.velocity += -Vector3.up * gravity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (rigidbody.velocity.magnitude > soundOutMagnitude)
        {
            soundController.Impact();
        }
    }
}
