using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartZone : MonoBehaviour
{
    private Scene loadedscene;
   
    void Start ()
    {
        loadedscene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(loadedscene.buildIndex);
    }

}
