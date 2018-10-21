using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    [SerializeField]private float speed = 10.0F;
    [SerializeField]private Rigidbody rb;
    [SerializeField]private Collider coll;
    [SerializeField]private float mouseSensitivity;
    [SerializeField]private Camera cam;

    [SerializeField]private float cameraClamp;
    private float currentRotation;
    [SerializeField]private float ground;
    [SerializeField] private float jumpForce;
    

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        
        cam = Camera.main;
        ground = coll.bounds.extents.y;
        
    }

    // Update is called once per frame
    void Update()
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

        //Jumping

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = Vector3.up * jumpForce;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (2 - 1) * Time.deltaTime;
        }
       

      

        

        //Rotation

        float _yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0, _yRotation, 0) * mouseSensitivity;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(_rotation));


        float _xRotation = Input.GetAxisRaw("Mouse Y");
        
        currentRotation -= _xRotation * mouseSensitivity;

        currentRotation = Mathf.Clamp(currentRotation, -cameraClamp, cameraClamp);

        cam.transform.localEulerAngles = new Vector3 (currentRotation, 0f, 0f);

    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, ground + 0.2f);
    }






}
