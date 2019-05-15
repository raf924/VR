using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onclick : MonoBehaviour, IInputClickHandler, IInputHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("Coucou");
    }

    public void OnInputDown(InputEventData eventData)
    {
        Debug.Log("Coucou");
    }

    public void OnInputUp(InputEventData eventData)
    {
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
