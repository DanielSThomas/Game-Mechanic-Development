using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{

    [SerializeField] int nextLevelIndex;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(nextLevelIndex);
    }

    
}
