using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyLine : MonoBehaviour
{
    private LineRenderer copyTo;
    public Rope rope;
    void Start()
    {
        copyTo = GetComponent <LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rope.lastConnectedRopeSegment is null)
        {
            copyTo.positionCount = 0;
            for (int j = 1; j < rope.transforms.Count; j++)
            {
                Transform t = rope.transforms[j];
                t.GetComponent<RopeSegment>().electrified = false;
            }
            return;
        }
        int i = rope.transforms.IndexOf(rope.lastConnectedRopeSegment.transform);
        copyTo.positionCount = rope.transforms.Count - i;
        Vector3[] ps = new Vector3[copyTo.positionCount];
        for (int j = 0; j < copyTo.positionCount; j++)
        {
            Transform t = rope.transforms[rope.transforms.Count - 1 - j];
            t.GetComponent<RopeSegment>().electrified = true;
            ps[j] = rope.transforms[rope.transforms.Count - 1 - j].position;
        }
        for(int j = copyTo.positionCount; j < rope.transforms.Count-1; j++)
        {
            Transform t = rope.transforms[rope.transforms.Count - 1 - j];
            t.GetComponent<RopeSegment>().electrified = false;
        }
        copyTo.SetPositions(ps);
    }
}
