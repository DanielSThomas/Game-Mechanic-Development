using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMarker : MonoBehaviour
{
    // Variables---------------------------------------------------------------
    [SerializeField] private GameObject instanciateOB;
    [SerializeField] private Transform instanciateLocation;


    private void Update()
    {
        if(Input.GetButtonDown("Record"))
        {
            InstanciateObject();
        }

    }

    // Methods------------------------------------------------------------------

    private void InstanciateObject()
    {
        GameObject _copy = Instantiate(instanciateOB, instanciateLocation);

        Destroy(_copy, 5);
    }  

}
