using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    [SerializeField]private float speed = 8F;
    [SerializeField]private Rigidbody rb;  
    [SerializeField]private float mouseSensitivity;
    [SerializeField]private Camera cam;

    [SerializeField]private float cameraClamp;
    private float currentRotation;
    
    [SerializeField] private float jumpForce;
    [SerializeField] LayerMask raycastMask;
    [SerializeField] private bool grounded;


    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        
        cam = Camera.main;
        grounded = false;


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
            rb.MovePosition(rb.position + _velocity * Time.deltaTime);
        }
        

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //Jumping

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
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

        Debug.DrawRay(transform.position + new Vector3(0, -0.8f, 0), -Vector3.up * 0.5f, Color.blue);
    }

    

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0,-0.8f,0), -Vector3.up, 0.5f, raycastMask);      
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if(Vector3.Angle(contact.normal, Vector3.up) < 60)
            {
                grounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }






}
