using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon : MonoBehaviour
{
    public RopeSegment attachedRopeSegment;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject != NewPlayer.Instance.gameObject)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ConnectPylon();
        }
    }

    void ConnectPylon()
    {
        attachedRopeSegment = NewPlayer.Instance.rope.AttachDetachPylon(this);
    }
}
