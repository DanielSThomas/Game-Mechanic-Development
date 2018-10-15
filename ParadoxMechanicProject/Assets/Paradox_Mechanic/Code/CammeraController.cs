using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CammeraController : MonoBehaviour
{
    private Vector2 mouseLook;
    private Vector2 smoothV;
    [SerializeField]private float sensitivity = 5.0f;
    [SerializeField]private float smoothing = 2.0f;

    private Vector2 recordedCamPoint;
    [SerializeField]private bool localIsRecording;

    [SerializeField] private GameObject character;

	// Use this for initialization
	void Start ()
    {
        character = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV * sensitivity;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        

        //Restarting Play Rotation
        if(Input.GetMouseButtonDown(0) && localIsRecording == false)
        {
            recordedCamPoint = mouseLook;
            Invoke("RestartCam", 5);
            localIsRecording = character.GetComponent<PlayerRecording>().GetisRecording;
        }


        //Free Mouse
        if (Input.GetKey(KeyCode.M))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void RestartCam()
    {
        mouseLook = recordedCamPoint;
        localIsRecording = character.GetComponent<PlayerRecording>().GetisRecording;
    }
}
