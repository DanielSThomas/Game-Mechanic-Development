using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerRecording : MonoBehaviour
{

    // VisualEffect Variables -------------------------------------------------
    [SerializeField]private PostProcessingProfile profileNormal;
    [SerializeField]private PostProcessingProfile profileRecording;
    [SerializeField]private PostProcessingBehaviour ppb;

    [SerializeField]private Text recordText;

    // Variables---------------------------------------------------------------
    [SerializeField] private float recordAccuracy;
    [SerializeField] private float recordTime; 

    [SerializeField] private List<Vector3> recordedPoints;
    [SerializeField] private List<Quaternion> recordedRotation;
    private Vector3 point;
    private Quaternion rotation;

    private float timer; // Should optimise this in some way. Atm the timer is always running :/
    private bool isRecording = false;
    [SerializeField]private bool recordcooldown = false;

    private ParadoxCreator paradoxCreator;

    [SerializeField] private GameObject markerOB;
    GameObject markerCopy;

    private Scene loadedscene;

    // Initialization---------------------------------------------------------
    void Start () 
    {
        paradoxCreator = GetComponent<ParadoxCreator>();

        recordText.enabled = false;

        loadedscene = SceneManager.GetActiveScene();

    }
	
	// Update-----------------------------------------------------------------
	void Update ()
    {

        Recording();

        Timer();

        //Restart Scene
        if (Input.GetButton("Restart") && isRecording == false)
        {
            SceneManager.LoadScene(loadedscene.buildIndex);
        }
    }

    // Methods----------------------------------------------------------------
    #region Private Methods

    private void Recording()
    {
        if (Input.GetButton("Record") && isRecording == false && recordcooldown == false)
        {
            StartCoroutine("RecordPoints");
            recordedPoints.Clear();
            recordedRotation.Clear();
            isRecording = true;
            recordcooldown = true;

            Invoke("RecordEnd", recordTime);

            ppb.profile = profileRecording;
            recordText.enabled = true;
            timer = 5f;

            markerCopy = Instantiate(markerOB, transform.position, transform.rotation);
        }

        //Override Recording
        if (Input.GetButton("Record") && isRecording == true && timer <4.5f && timer >0) //The timer varrible here is being used as a input buffer to prevent both actions happeing at once.
        {
            RecordEnd();
            CancelInvoke(); // Best Built in Method
            Invoke("RestartCoolDown", 0.5f); // For some reason this is not being called in the RecordEnd() when going though the Override. So I am calling it again here.
        }

    }

    void RecordEnd()
    {
        if (isRecording == true)
        {
            StopCoroutine("RecordPoints");

            Invoke("RestartCoolDown", 0.5f);

            isRecording = false;
            transform.position = recordedPoints[0];
            transform.rotation = recordedRotation[0];
            paradoxCreator.CreateClone();

            ppb.profile = profileNormal;
            recordText.enabled = false;

            Destroy(markerCopy);
            
        }
        else
            return;

    }

    private void Timer() // Simple timer
    {
        timer -= Time.deltaTime;
        recordText.text = timer.ToString("0");
    }

    private void RestartCoolDown() // Again used to buffer the input
    {
        recordcooldown = false;
    }

    


    #endregion

    #region Public Methods
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

    #endregion

    // Coroutines-------------------------------------------------------------
    IEnumerator RecordPoints()
    {
        for (; ; )
        {
            point = transform.position;

            rotation = transform.rotation;

            recordedPoints.Add(point);
            recordedRotation.Add(rotation);

            Debug.DrawRay(point, transform.up, Color.red, 15);

            yield return new WaitForSeconds(recordAccuracy);
    
        }
    }
}
