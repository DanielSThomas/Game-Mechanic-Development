using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    [SerializeField]private float speed = 10.0F;
    [SerializeField]private Rigidbody rb;
    [SerializeField]private Collider coll;
    [SerializeField]private float mouseSensitivity;
    [SerializeField]private Camera camera;

    [SerializeField]private float cameraClamp;
    private float currentRotation;
    [SerializeField]private float ground;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool canJump;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        camera = Camera.main;
        ground = coll.bounds.extents.y;
        canJump = true;
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

        //Jumping

        if (Input.GetKeyDown(KeyCode.Space) && canJump == true && IsGrounded())
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            Invoke("JumpCooldown", 1.1f);
        }

        

        //Rotation

        float _yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0, _yRotation, 0) * mouseSensitivity;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(_rotation));


        float _xRotation = Input.GetAxisRaw("Mouse Y");
        
        currentRotation -= _xRotation * mouseSensitivity;

        currentRotation = Mathf.Clamp(currentRotation, -cameraClamp, cameraClamp);

        camera.transform.localEulerAngles = new Vector3 (currentRotation, 0f, 0f);

    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, ground + 0.1f); 
    }

    private void JumpCooldown()
    {
        canJump = true;
    }

    


}
