using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateToolSet : MonoBehaviour
{

	public float scaleFactor = 0.4f;
	GameObject cursor;
	bool isActive;

	public List<SpriteRenderer> rend;

    // Start is called before the first frame update
    void Start()
    {
		cursor = GameObject.Find("DefaultCursor");   
		transform.localScale *= scaleFactor;
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = cursor.transform.position;
		transform.rotation = cursor.transform.rotation;
    }
}
