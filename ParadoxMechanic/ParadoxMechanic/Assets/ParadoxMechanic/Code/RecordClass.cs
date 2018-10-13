using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RecordClass
{
    private Vector3 point;

    public RecordClass(List<Vector3> _recordedPoints, Vector3 _point)
    {       
        RecordedPoints = _recordedPoints;
        point = _point;
    }

    public List<Vector3> RecordedPoints { get; set; }

    public Vector3 Point { get; set; }
}
