using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    [SerializeField]private float speed = 10.0F;
    [SerializeField]private Rigidbody rb;
    [SerializeField]private float mouseSensitivity;
    [SerializeField]private Camera camera;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movement

        float _yMovement = Input.GetAxis("Vertical") * speed;
        float _xMovement = Input.GetAxis("Horizontal") * speed;

        Vector3 _verticalMovement = transform.forward * _yMovement;
        Vector3 _horizontalMovement = transform.right * _xMovement;


        Vector3 _velocity = (_horizontalMovement + _verticalMovement).normalized * speed;

      
       

        if (_velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + _velocity * Time.fixedDeltaTime);
        }
        

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //Rotation

        float _yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0, _yRotation, 0) * mouseSensitivity;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(_rotation));


        float _xRotation = Input.GetAxisRaw("Mouse Y");

        Vector3 _camRotation = new Vector3(_xRotation, 0, 0) * mouseSensitivity;

        camera.transform.Rotate(-_camRotation);

    }
    


}
