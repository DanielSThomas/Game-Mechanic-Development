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
    private bool isGrounded;

    [SerializeField] LayerMask raycastMask;

    [SerializeField] Renderer Obrenderer;

    private Rigidbody rb;

    private Vector3 _lastvel;
    private bool cooldown = false;

    private bool dashactive;
    private bool crashed;
    
    
    [SerializeField]private bool chainActive = false;
    
    [SerializeField]private float chainWindow;

    private float minDashWindow;
    private float maxDashWindow;

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

        ChainWindow();

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
        float _zMovement = Input.GetAxis("Vertical");
        float _xMovement = Input.GetAxis("Horizontal");

        Vector3 _forward = Camera.main.transform.forward.normalized;

        Vector3 _right = Camera.main.transform.right.normalized;

        Vector3 _verticalMovement = _forward * _zMovement * 1.3f; //Movement compensation

        _verticalMovement.y = 0;

        Vector3 _horizontalMovement = _right * _xMovement;

        Vector3 _velocity = _horizontalMovement + _verticalMovement;

        Vector3 _DashVelocity = _horizontalMovement * 9 + _verticalMovement * 9;

        //Vector3 _velocity = new Vector3(_xMovement* speed,0, _zMovement* speed * 1.3f);
        
        

        //Movement
        if (_velocity != Vector3.zero && dashactive == false && crashed == false)
        {
            rb.MovePosition(rb.position + _velocity * speed* Time.deltaTime);
            //_lastvel = _velocity;
            _lastvel = _DashVelocity;


        }
        else if(dashactive == true && crashed == false)
        {
           
            rb.MovePosition(rb.position + _lastvel * Time.deltaTime * 5);
            
        }
     
        else if(crashed == true)
        {
            rb.MovePosition(rb.position + Vector3.zero);
        }
       
        //Rotation
        if (_velocity != Vector3.zero && dashactive == false && crashed == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_velocity), 0.5F);
        }

    }

   

    private void Dash()
    {
        if (cooldown == false && crashed == false && chainActive == false)
        {
            Obrenderer.material.color = Color.blue;
        }
        else if (cooldown == true && crashed == false && chainActive == true)
        {
            Obrenderer.material.color = Color.cyan;
        }



        if (Input.GetButtonDown("Jump") && cooldown == false && crashed == false && chainActive == false && isGrounded == true)
        {
            speed = 0.1f;
            
            rb.useGravity = false;
            Invoke("DashEnd", 0.15f);
            Invoke("CooldownEnd", 0.41f);
            
            dashactive = true;
            cooldown = true;
            minDashWindow = 0.3f;
            maxDashWindow = 0.4f;

            chainWindow = 0;
            chainActive = true;
        }
        else if (Input.GetButtonDown("Jump") && chainActive == true && crashed == false && chainWindow > minDashWindow && chainWindow < maxDashWindow && isGrounded == true)
        {
            speed = 0.1f;
            dashactive = true;
            chainWindow = 0;
            CancelInvoke();                 
            Invoke("DashEnd", 0.15f);
            Invoke("CooldownEnd", 0.4f);
            
        }
        else if (Input.GetButtonDown("Jump") && crashed == false && chainActive == true && chainWindow < minDashWindow)
        {
            
            chainActive = false;
            
        }
        



    }

    private void ChainWindow()
    {   
        if (chainActive == true)
        {
            chainWindow += Time.deltaTime;
            if (chainWindow > 0.42f)
            {             
                chainActive = false;
            }
        }
        

        
    }


    private void DashEnd()
    {
        
        rb.useGravity = true;
        dashactive = false;
        
        
    }

 


    private void CooldownEnd()
    {
        cooldown = false;
        speed = 6;
    }

    private void CrashEnd()
    {
        crashed = false;             
    }

 

    private void OnTriggerEnter(Collider other)
    {
        if (dashactive == true && crashed == false && other.tag == "wall")
        {
            
            //CancelInvoke();
            chainActive = false;
            crashed = true;

            Invoke("CrashEnd", 0.5f);
            Obrenderer.material.color = Color.red;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Angle(contact.normal, Vector3.up) < 60)
            {
                isGrounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

   



}
