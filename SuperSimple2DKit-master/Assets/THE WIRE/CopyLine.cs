using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyLine : MonoBehaviour
{
    public LineRenderer copyFrom;
    private LineRenderer copyTo;
    void Start()
    {
        copyTo = GetComponent <LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] positions = new Vector3[copyFrom.positionCount];
        copyFrom.GetPositions(positions);
        copyTo.positionCount = copyFrom.positionCount;
        copyTo.SetPositions(positions);
    }
}
