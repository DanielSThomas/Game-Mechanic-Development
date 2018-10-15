using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class HoldPlayer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider col)
        {
            col.transform.parent = gameObject.transform;
        }

        private void OnTriggerExit(Collider col)
        {
            col.transform.parent = null;
        }

     
    }
