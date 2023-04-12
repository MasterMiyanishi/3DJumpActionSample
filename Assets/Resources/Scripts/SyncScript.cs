using System.Collections.Generic;
using UnityEngine;

public class SyncScript : MonoBehaviour {

    [SerializeField]
    GameObject syncObject;

    [SerializeField]
    bool positionSync = true;

    [SerializeField]
    bool rotationSync = true;

    [SerializeField]
    Vector3 offsetPosition;


    void Update () {
        if (positionSync)
        {
            this.transform.position = syncObject.transform.position + offsetPosition;
        }
        if (rotationSync)
        {
            this.transform.rotation = syncObject.transform.rotation;
        }
    }
}
