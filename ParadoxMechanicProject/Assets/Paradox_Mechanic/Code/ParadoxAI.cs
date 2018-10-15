using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxAI : MonoBehaviour
{
    [SerializeField]private List<Vector3> localRecordedPoints;
    [SerializeField]private List<Quaternion> localRotationPoints;

    private PlayerRecording pr;

    [Range(0.01f,0.1f)]
    [SerializeField] private float accuracy; 

    // Use this for initialization
    void Start ()
    {
        pr = GameObject.Find("Player").GetComponent<PlayerRecording>();

        localRotationPoints = pr.CreateCopyRotationList();

        localRecordedPoints = pr.CreateCopyPointList();
        
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

            if(localRotationPoints.Count > 0)
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
             
            yield return new WaitForSeconds(accuracy);

        }

    }
}
