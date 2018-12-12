using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThirdPersonMovement : MonoBehaviour
{

    // Visual Variables--------------------------------------------------------

    [SerializeField]private Animator animator;



    // Variables---------------------------------------------------------------
    [SerializeField]private float speed = 6;
    [SerializeField]private float jumpForce = 5;

    [SerializeField] LayerMask raycastMask;

    private Rigidbody rb;

    private Vector3 _lastvel;

    private bool dashactive;

    // Initialization----------------------------------------------------------
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        
       


       

    }

    // Update------------------------------------------------------------------
    void Update()
    {

        //Movement
        Movement();

        //Dash
        Dash();

        //Unlock Mouse
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Methods----------------------------------------------------------------
    

    private void Movement()
    {
       // _lastvel = Vector3.zero;
        float _zMovement = speed * Input.GetAxis("Vertical");
        float _xMovement = speed * Input.GetAxis("Horizontal");


        Vector3 _velocity = new Vector3(_xMovement,0, _zMovement);
        
        

        //Movement
        if (_velocity != Vector3.zero && dashactive == false)
        {
            rb.MovePosition(rb.position + _velocity * Time.deltaTime);
            _lastvel = _velocity;

            
        }
        else if(dashactive == true)
        {
            rb.MovePosition(rb.position + _lastvel * Time.deltaTime * speed);
        }
            



        //Rotation
        if (_velocity != Vector3.zero && dashactive == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_velocity), 0.5F);
        }

    }

    private void Dash()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //speed = 14;
            rb.useGravity = false;
            Invoke("DashEnd", 0.2f);
            dashactive = true;
        }
         
    }

    private void DashEnd()
    {
        speed = 6;
        rb.useGravity = true;
        dashactive = false;
    }

   


}
