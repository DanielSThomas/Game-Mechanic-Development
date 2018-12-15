using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour
{

    [SerializeField] private float smooth = 2.0f;
    private Quaternion targetRotation;
    private bool cooldown;

    // Use this for initialization
    void Start ()
    {
        targetRotation = transform.rotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetAxis("Mouse X") > 0.7f && cooldown == false)
        {
            targetRotation *= Quaternion.AngleAxis(45, Vector3.up);
            cooldown = true;
            Invoke("ResetCooldown", 0.3f);
        }
       
        if (Input.GetAxis("Mouse X") < -0.7 && cooldown == false)
        {
            targetRotation *= Quaternion.AngleAxis(-45, Vector3.up);
            cooldown = true;
            Invoke("ResetCooldown", 0.3f);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * smooth * Time.deltaTime);
    }

    private void ResetCooldown()
    {
        cooldown = false;
    }

    
}
