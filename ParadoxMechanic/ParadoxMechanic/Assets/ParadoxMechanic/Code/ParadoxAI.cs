using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxAI : MonoBehaviour
{
    [SerializeField]private List<Vector3> localRecordedPoints;
    [SerializeField]private List<Quaternion> localRotationPoints;

    // Use this for initialization
    void Start ()
    {
        localRecordedPoints = GameObject.Find("Player").GetComponent<PlayerRecording>().GetPoints;
        localRotationPoints = GameObject.Find("Player").GetComponent<PlayerRecording>().GetRotationPoints;
        StartCoroutine("Movement");
    }
	
	// Update is called once per frame
	void Update ()
    { 
       
        
    }

    IEnumerator Movement()
    {
        for(; ; )
        { 

            if(localRecordedPoints.Count > 0)
            {
                transform.position = localRecordedPoints[0];
                transform.rotation = localRotationPoints[0];
                localRecordedPoints.RemoveAt(0);
                localRotationPoints.RemoveAt(0);
            }
            else
            {
                StopCoroutine("Movement");
            }
             
            yield return new WaitForSeconds(0.02f);

        }

    }
}
