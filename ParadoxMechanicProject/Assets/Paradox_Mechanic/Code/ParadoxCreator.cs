using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxCreator : MonoBehaviour
{

    [SerializeField]private GameObject instanciateOB;
    [SerializeField]private Transform instanciateLocation;
    [SerializeField]private float lifeTime;
    
	// Use this for initialization
	void Start ()
    {

        
    }
	
	// Update is called once per frame
	void Update ()
    {
       
       
            
	}

    public void CreateClone()
    {
        InstanciateObject();
    }

    private void InstanciateObject()
    {
        GameObject _copy = Instantiate(instanciateOB, instanciateLocation);
        Destroy(_copy, lifeTime);
    }
}
