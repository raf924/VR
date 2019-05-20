using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void changeMenu()
    {
        var holoMenu = GameObject.Find("HoloMenu");
        //holoMenu.transform.localScale = 3.0f;
        holoMenu.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
        holoMenu.transform.rotation = new Quaternion(0.0f, Camera.main.transform.rotation.y, 0.0f, Camera.main.transform.rotation.w);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
