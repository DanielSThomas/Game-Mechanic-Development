using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    [SerializeField]private float speed = 10.0F;

  
    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float _verticalMovement = Input.GetAxis("Vertical") * speed;
        float _horizontalMovement = Input.GetAxis("Horizontal") * speed;
        

        Vector3 _movement = new Vector3(_horizontalMovement, 0, _verticalMovement);

        _movement = Vector3.ClampMagnitude(_movement, speed);
        _movement *= Time.deltaTime;
         
        transform.Translate(_movement);

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    

    }
    


}
