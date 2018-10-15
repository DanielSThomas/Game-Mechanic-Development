using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerRecording : MonoBehaviour
{

    private Scene loadedscene;

    //VisualEffects Varribles
    [SerializeField]private PostProcessingProfile profileNormal;
    [SerializeField]private PostProcessingProfile profileRecording;
    [SerializeField]private PostProcessingBehaviour ppb;

    [SerializeField] private Text recordText;

    //Varribles
    [SerializeField] private List<Vector3> recordedPoints;
    [SerializeField] private List<Quaternion> recordedRotation;

    private Vector3 point;
    private Quaternion rotation;

    [SerializeField] private float recordAccuracy;
    //[SerializeField] private float maxPoints;

    [SerializeField] private float recordTime;

    private bool isRecording = false;
    private ParadoxCreator paradoxCreator;

    [SerializeField] private Transform respawn;

    //Gets
    public bool GetisRecording
    {
        get
        {
            return isRecording;
        }
    }


    public List<Vector3> CreateCopyPointList()
    {
        List<Vector3> _localList = new List<Vector3>(recordedPoints);

        for (int i = 0; i < recordedPoints.Count; i++)
        {
            _localList[i] = recordedPoints[i];
        }

        return _localList;
    }

    public List<Quaternion> CreateCopyRotationList()
    {
        List<Quaternion> _localList = new List<Quaternion>(recordedRotation);

        for (int i = 0; i < recordedRotation.Count; i++)
        {
            _localList[i] = recordedRotation[i];
        }

        return _localList;
    }


    // Use this for initialization
    void Start () 
    {
        paradoxCreator = GetComponent<ParadoxCreator>();

        recordText.enabled = false;

        loadedscene = SceneManager.GetActiveScene();

    }
	
	// Update is called once per frame
	void Update ()
    {
        

		if(Input.GetMouseButtonDown(0) && isRecording == false) 
        { 
            StartCoroutine("RecordPoints");
            recordedPoints.Clear();
            recordedRotation.Clear();
            isRecording = true;
            Invoke("RecordEnd", recordTime);

            ppb.profile = profileRecording;
            recordText.enabled = true;

           
        }

        if(Input.GetKeyDown(KeyCode.R) && isRecording == false)
        {
            // transform.position = respawn.position;
            SceneManager.LoadScene(loadedscene.buildIndex);
        }


    }

    void RecordEnd()
    {
        StopCoroutine("RecordPoints");
        
        
        isRecording = false;
        transform.position = recordedPoints[0];
        
        paradoxCreator.CreateClone();

        ppb.profile = profileNormal;
        recordText.enabled = false;
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
