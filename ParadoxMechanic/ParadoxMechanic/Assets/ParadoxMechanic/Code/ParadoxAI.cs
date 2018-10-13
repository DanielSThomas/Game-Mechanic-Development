using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxAI : MonoBehaviour
{ 
    private List<Vector3> localRecordList;

    // Use this for initialization
    void Start ()
    {
        localRecordList = GameObject.Find("Player").GetComponent<PlayerRecording>().record.RecordedPoints;
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

            if(localRecordList.Count > 1)
            {
                transform.position = localRecordList[0];

                localRecordList.RemoveAt(0);
            }
            else
            {
                StopCoroutine("Movement");
            }

            yield return new WaitForSeconds(0.02f);

        }

    }
}
