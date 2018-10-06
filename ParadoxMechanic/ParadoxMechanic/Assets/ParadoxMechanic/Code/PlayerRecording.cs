using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecording : MonoBehaviour
{

    [SerializeField] private List<Vector3> recordedPoints;
    private Vector3 point;
    [SerializeField] private float recordAccuracy;
    [SerializeField] private float maxPoints;
    
   
    public List<Vector3> recordedPointsCopy;
    
   

    // Use this for initialization
    void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.R))
        { 
            StartCoroutine("RecordPoints");
            recordedPoints.Clear();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StopCoroutine("RecordPoints");
            recordedPointsCopy = recordedPoints;
        }
        

    }


    IEnumerator RecordPoints()
    {
        for(; ; )
        {
            
            if(recordedPoints.Count >= maxPoints)
            {
                recordedPoints.RemoveAt(0);
            }

            point = this.transform.position;

            recordedPoints.Add(point);

            Debug.DrawRay(point, transform.up, Color.red, 15);

            yield return new WaitForSeconds(recordAccuracy);
         
            


        }
        
    }
   
}
