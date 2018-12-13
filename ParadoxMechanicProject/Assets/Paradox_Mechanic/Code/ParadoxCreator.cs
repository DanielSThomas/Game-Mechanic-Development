using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxCreator : MonoBehaviour
{
    // Variables---------------------------------------------------------------
    [SerializeField]private GameObject instanciateOB;
    [SerializeField]private Transform instanciateLocation;
    [SerializeField]private int maxRecords;

    [SerializeField]private List<GameObject> allinstanciates;
    [SerializeField] private int recordCounter;

    // Update------------------------------------------------------------------
    void Update ()
    {

        if (recordCounter > maxRecords)
        {
            Destroy(allinstanciates[0]);
            allinstanciates.RemoveAt(0);
            recordCounter -= 1;
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

    public void IncreaseCounter()
    {
        recordCounter += 1;
    }

    
}
