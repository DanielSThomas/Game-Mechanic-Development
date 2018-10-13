using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecording : MonoBehaviour
{
    private List<Vector3> recordedPoints;
    private Vector3 point;

    public RecordClass record;

    [SerializeField] private float recordAccuracy;
    [SerializeField] private float maxPoints;
    [SerializeField] private float recordTime;
    private bool isRecording = false;
    private ParadoxCreator paradoxCreator;
    
     
    public List<Vector3> recordedPointsCopy; 
    
   

    // Use this for initialization
    void Start () 
    {
        record = new RecordClass(recordedPoints, point);

        paradoxCreator = GetComponent<ParadoxCreator>();
	}

	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.R) && isRecording == false) 
        { 
            StartCoroutine("RecordPoints");
            recordedPoints.Clear();           
            isRecording = true;
            Invoke("RecordEnd", recordTime);        
        }
             
    }

    void RecordEnd()
    {
        StopCoroutine("RecordPoints");
        recordedPointsCopy = recordedPoints;
        isRecording = false;
        this.transform.position = recordedPoints[0];
        paradoxCreator.CreateClone();
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
