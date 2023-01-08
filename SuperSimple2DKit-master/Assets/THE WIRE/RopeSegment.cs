using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    public GameObject connectedAbove, connectedBelow;
    public bool deletable = false;
    public bool undeletable = false;
    public bool electrified = false;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        Inst();
        StartCoroutine(CountdownToDeletable(.01f));
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (electrified)
        {
            sr.color = Color.red;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    public void Inst()
    {
        //Debug.Log("Start()"+Random.value);
        connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();
        if (aboveSegment != null)
        {
            aboveSegment.connectedBelow = gameObject;
            float spriteBottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom * -1);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
    }

    public IEnumerator CountdownToDeletable(float time)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(time);
        if(!undeletable)
            deletable = true;
    }

}
