using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecording : MonoBehaviour
{

    [SerializeField] private List<Vector3> recordedPoints;
    [SerializeField] private Vector3 point;
    [SerializeField] private float recordAccuracy;

    public List<Vector3> recordedPointsCopy;
    
    // Use this for initialization
    void Start ()
    {
        StartCoroutine("RecordPoints");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    IEnumerator RecordPoints()
    {
        for(; ; )
        {
            point = this.transform.position;

            recordedPoints.Add(point);

            yield return new WaitForSeconds(recordAccuracy);

            Debug.DrawRay(point, transform.up, Color.red, 10);

            recordedPointsCopy = recordedPoints;
        }
        
    }
}
