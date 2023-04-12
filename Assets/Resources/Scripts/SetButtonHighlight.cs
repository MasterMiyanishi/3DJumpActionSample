using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetButtonHighlight : MonoBehaviour {

	void Start ()
    {
        var eventSystem = GameObject.FindObjectOfType<EventSystem>();

        eventSystem.enabled = true;
        eventSystem.SetSelectedGameObject(this.gameObject);
	}
	
}
