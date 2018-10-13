using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecording : MonoBehaviour
{

    [SerializeField] private List<Vector3> recordedPoints;
    [SerializeField] private List<Quaternion> recordedRotation;
    private Vector3 point;
    private Quaternion rotation;
    [SerializeField] private float recordAccuracy;
    [SerializeField] private float maxPoints;
    [SerializeField] private float recordTime;
    private bool isRecording = false;
    private ParadoxCreator paradoxCreator;
    

    public List<Vector3> recordedPointsCopy;
    public List<Quaternion> recordedRotationCopy;


    // Use this for initialization
    void Start () 
    {
        paradoxCreator = GetComponent<ParadoxCreator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.R) && isRecording == false) 
        { 
            StartCoroutine("RecordPoints");
            recordedPoints.Clear();
            recordedRotation.Clear();
            isRecording = true;
            Invoke("RecordEnd", recordTime);        
        }
             
    }

    void RecordEnd()
    {
        StopCoroutine("RecordPoints");
        recordedPointsCopy = recordedPoints;
        recordedRotationCopy = recordedRotation;
        isRecording = false;
        transform.position = recordedPoints[0];
        
        paradoxCreator.CreateClone();
    }
    


    IEnumerator RecordPoints()
    {
        for(; ; )
        {
            
            //if(recordedPoints.Count >= maxPoints)
            //{
            //    recordedPoints.RemoveAt(0);
            //}

            point = transform.position;

            
            rotation = transform.rotation;
             
            recordedPoints.Add(point);
            recordedRotation.Add(rotation);

            Debug.DrawRay(point, transform.up, Color.red, 15);

            yield return new WaitForSeconds(recordAccuracy);
   

        }
        
    }

    
   
}
