using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCamera : MonoBehaviour {
    
    float cameraRotationX = 10;
    float cameraRotationY = 10;

    [SerializeField]
    bool cameraRotationXChenge = true;
    [SerializeField]
    bool cameraRotationYChenge = true;

    [SerializeField]
    Vector2 cameraRotationLimit;

    [SerializeField]
    Vector3 cameraOffset;

    void Update () {
        if (cameraRotationXChenge)
        {
            cameraRotationX += Input.GetAxis("RHorizontal");
        }
        if (cameraRotationYChenge)
        {
            cameraRotationY += Input.GetAxis("RVertical");
        }
        if (cameraRotationX > cameraRotationLimit.x ||
            cameraRotationX < -cameraRotationLimit.x)
        {
            cameraRotationX = cameraRotationLimit.x * Mathf.Sign(cameraRotationX);
        }
        if (cameraRotationY > cameraRotationLimit.y ||
           cameraRotationY < -cameraRotationLimit.y)
        {
            cameraRotationY = cameraRotationLimit.y * Mathf.Sign(cameraRotationY);
        }
        this.transform.rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, cameraOffset.z+ (Mathf.Abs(cameraRotationY) / 20));
    }
}
