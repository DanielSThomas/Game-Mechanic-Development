using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour {

    private float rotatespeed = 2.0f;
   
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float h = rotatespeed * Input.GetAxis("Mouse X");

       

        transform.Rotate(0, h, 0);
    }

    
}
