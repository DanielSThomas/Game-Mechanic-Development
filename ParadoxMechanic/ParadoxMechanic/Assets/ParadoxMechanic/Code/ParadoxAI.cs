using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxAI : MonoBehaviour
{
    [SerializeField]private List<Vector3> localRecordedPoints;

    // Use this for initialization
    void Start ()
    {
        localRecordedPoints = GameObject.Find("Player").GetComponent<PlayerRecording>().recordedPointsCopy;
        StartCoroutine("Movement");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //foreach (Vector3 point in localRecordedPoints)
        //{
        //    transform.Translate(point.x, point.y, point.z);
        //}
        
    }

    IEnumerator Movement()
    {
        for(; ; )
        {
            try
            {
                transform.position = localRecordedPoints[0];
                localRecordedPoints.RemoveAt(0);
                
            }
           catch
            {
                StopCoroutine("Movement");
            }
            yield return new WaitForSeconds(0.05f);
        }

    }
}
