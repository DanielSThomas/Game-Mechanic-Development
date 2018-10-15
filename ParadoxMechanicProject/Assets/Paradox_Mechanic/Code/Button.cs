using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    [SerializeField]private GameObject target;
    [SerializeField]private bool active = false;

	// Use this for initialization
	void Start ()
    {
		
	}

    private void Update()
    {
        if (active == true)
        {
            target.SetActive(false);
        }
        else
            target.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        active = false;
	}

    private void OnTriggerStay(Collider other)
    {
        active = true;
    }

    


    //private void OnTriggerEnter(Collider other)
    //{
    //    target.SetActive(false);
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    target.SetActive(true);
    //}

}
