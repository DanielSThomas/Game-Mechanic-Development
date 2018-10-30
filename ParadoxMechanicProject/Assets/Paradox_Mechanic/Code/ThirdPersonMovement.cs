using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    // Variables---------------------------------------------------------------
    [SerializeField]private float speed = 6;
    [SerializeField]private float jumpForce = 5;

    [SerializeField] LayerMask raycastMask;

    private Rigidbody rb;
    
    private bool grounded;

    // Initialization----------------------------------------------------------
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        
        grounded = false;
        
    }

    // Update------------------------------------------------------------------
    void Update()
    {

        //Movement
        Movement();

        //Jumping
        Jump();

        //Unlock Mouse
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Methods----------------------------------------------------------------
    #region Private Methods

    private void Movement()
    {
        float _zMovement = speed * Input.GetAxis("Vertical");
        float _xMovement = speed * Input.GetAxis("Horizontal");

        Vector3 _forward = Camera.main.transform.forward.normalized;

        Vector3 _right = Camera.main.transform.right.normalized;

        Vector3 _verticalMovement = _forward * _zMovement * 1.6f; //Movement compensation

        _verticalMovement.y = 0;

        Vector3 _horizontalMovement = _right * _xMovement;
    
        Vector3 _velocity = _horizontalMovement + _verticalMovement;

        //Movement
        if (_velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + _velocity * Time.deltaTime);
        }

        //Rotation
        if (_velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_velocity), 0.15F);
        }

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            rb.velocity = Vector3.up * jumpForce;
        }

        //When we reach the peak of our jump, Increase gravity. (Used to make less floaty jumping)
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (2 - 1) * Time.deltaTime;
        }

        //This makes it so we hold space we jump higher
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (2 - 1) * Time.deltaTime;
        }
    }

    #region GroundingChecks
      
    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts) //Get all collider contacts
        {
            if(Vector3.Angle(contact.normal, Vector3.up) < 60) //Any contacts that are facing upwards within a given angle
            {
                grounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }

    #endregion

#endregion

}
