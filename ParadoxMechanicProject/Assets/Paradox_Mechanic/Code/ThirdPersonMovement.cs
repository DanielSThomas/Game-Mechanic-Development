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

    [SerializeField] Renderer Obrenderer;

    private Rigidbody rb;

    private Vector3 _lastvel;
    private bool cooldown = false;

    private bool dashactive;
    private bool crashed;
    
    [SerializeField]private bool chainActive = false;
    [SerializeField]private float chain;
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
        float _zMovement = speed* 1.2f * Input.GetAxis("Vertical");
        float _xMovement = speed * Input.GetAxis("Horizontal");


        Vector3 _velocity = new Vector3(_xMovement,0, _zMovement);
        
        

        //Movement
        if (_velocity != Vector3.zero && dashactive == false && crashed == false)
        {
            rb.MovePosition(rb.position + _velocity * Time.deltaTime);
            _lastvel = _velocity;

            
        }
        else if(dashactive == true && crashed == false)
        {
            rb.MovePosition(rb.position + _lastvel * Time.deltaTime * speed * 1.5f);
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



        if (Input.GetButtonDown("Jump") && cooldown == false && crashed == false && chainActive == false)
        {
            
            //speed = 14;
            rb.useGravity = false;
            Invoke("DashEnd", 0.15f);
            Invoke("CooldownEnd", 1f);
            dashactive = true;
            cooldown = true;
            minDashWindow = 0.4f;
            maxDashWindow = 0.6f;

            chainWindow = 0;
            chainActive = true;
        }
        else if (Input.GetButtonDown("Jump") && chainActive == true && crashed == false && chainWindow > minDashWindow && chainWindow < maxDashWindow)
        {
            dashactive = true;
            chainWindow = 0;
            CancelInvoke();
            if (minDashWindow > 0.2)
            {
                minDashWindow -= 0.02f;
                maxDashWindow -= 0.02f;
            }
            Invoke("DashEnd", 0.15f);
            Invoke("CooldownEnd", 1f);
            
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
            if (chainWindow > 0.7f)
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

  


}
