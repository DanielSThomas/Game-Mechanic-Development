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

    private float timer;
    private bool isRecording = false;
    [SerializeField]private bool recordcooldown = false;

    private ParadoxCreator paradoxCreator;

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
        }

        //Override Recording
        if (Input.GetButton("Record") && isRecording == true && timer <4.5f && timer >0)
        {
            RecordEnd();
            CancelInvoke();
            Invoke("RestartCoolDown", 0.5f);
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
        }
        else
            return;

    }

    private void Timer()
    {
        timer -= Time.deltaTime;
        recordText.text = timer.ToString("0");
    }

    private void RestartCoolDown()
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
