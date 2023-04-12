using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTransform : MonoBehaviour {

    [SerializeField]
    bool positionStop = true;

    [SerializeField]
    bool rotationStop = true;

    [SerializeField]
    bool scaleStop = true;

    Vector3 stopPosition;
    Quaternion stopRotation;
    Vector3 stopScale;

    void Start () {
        stopPosition = this.transform.localPosition ;
        stopRotation = this.transform.localRotation;
        stopScale = this.transform.localScale;

    }
	
	void Update () {
        if (positionStop)
        {
            this.transform.localPosition = stopPosition;
        }
        if (rotationStop)
        {
            this.transform.localRotation = stopRotation;
        }
        if (scaleStop)
        {
            this.transform.localScale = stopScale;
        }
    }
}
