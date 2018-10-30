using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxCreator : MonoBehaviour
{
    // Variables---------------------------------------------------------------
    [SerializeField]private GameObject instanciateOB;
    [SerializeField]private Transform instanciateLocation;
    [SerializeField]private int maxClones;

    [SerializeField]private List<GameObject> allinstanciates;

    // Update------------------------------------------------------------------
    void Update ()
    {

        if (allinstanciates.Count > maxClones)
        {
            Destroy(allinstanciates[0]);
            allinstanciates.RemoveAt(0);
        }

    }

    // Methods------------------------------------------------------------------

    private void InstanciateObject()
    {
        GameObject _copy = Instantiate(instanciateOB, instanciateLocation);

        allinstanciates.Add(_copy);

    }

    public void CreateClone()
    {
        InstanciateObject();
    }

    
}
