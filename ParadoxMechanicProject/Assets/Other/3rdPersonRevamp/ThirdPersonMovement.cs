using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
   
    [SerializeField]private float speed = 8F;
    [SerializeField]private Rigidbody rb;
    

    [SerializeField] private float jumpForce;
    [SerializeField] LayerMask raycastMask;
    [SerializeField] private bool grounded;

    
    

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        
      
        grounded = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Movement

        float _zMovement = speed * Input.GetAxis("Vertical");
        float _xMovement = speed * Input.GetAxis("Horizontal");


        Vector3 _forward = Camera.main.transform.forward.normalized;



        Vector3 _right = Camera.main.transform.right.normalized;

        Vector3 _verticalMovement = _forward * _zMovement *1.5f;

        _verticalMovement.y = 0;

        Vector3 _horizontalMovement = _right * _xMovement;

        _horizontalMovement.y = 0;



        Vector3 _velocity = _horizontalMovement + _verticalMovement;


        

        //Rotation

        if (_velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_velocity), 0.15F);
        }
        



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

        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            rb.velocity = Vector3.up * jumpForce;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (2 - 1) * Time.deltaTime;

            
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (2 - 1) * Time.deltaTime;
        }





        //Rotation

     
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
