using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxCreator : MonoBehaviour
{

    [SerializeField]private GameObject instanciateOB;
    [SerializeField]private Transform instanciateLocation;
    [SerializeField]private float lifeTime;

    [SerializeField]private List<GameObject> allinstanciates;
    [SerializeField]private int maxClones;
    
	// Use this for initialization
	void Start ()
    {
        
        
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (allinstanciates.Count > maxClones)
        {
            Destroy(allinstanciates[0]);
            allinstanciates.RemoveAt(0);
        }

    }

    public void CreateClone()
    {
        InstanciateObject();
    }

    private void InstanciateObject()
    {
        GameObject _copy = Instantiate(instanciateOB, instanciateLocation);

        allinstanciates.Add(_copy);

       
       //Destroy(_copy, lifeTime);
    }
}
