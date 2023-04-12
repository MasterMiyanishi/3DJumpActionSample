using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseNoClick : MonoBehaviour {

    public GameObject selectObj;
    void Start()
    {
        Screen.lockCursor = false;
    }
    void Update()
    {
        Screen.lockCursor = false;

        //クリックされた時 かつ lockStateがLockedではない時だけ実行
        if (Input.GetMouseButtonDown(0) && Cursor.lockState != CursorLockMode.Locked)
        {
            if(selectObj != null)
            {
                selectObj.GetComponent<Button>().Select();
            }
            return;
        }
    }
}
