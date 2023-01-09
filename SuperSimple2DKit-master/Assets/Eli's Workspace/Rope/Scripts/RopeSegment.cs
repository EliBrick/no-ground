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
        GetComponent<Rigidbody2D>().mass = NewPlayer.Instance.rope.segmentWeight*transform.localScale.x;
    }

    private void Update()
    {
        if (electrified)
        {
            sr.color = Color.red;
            gameObject.layer = 20;
        }
        else
        {
            sr.color = Color.white;
            gameObject.layer = 10;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision with" + collision.gameObject.name);
        if (collision.gameObject == NewPlayer.Instance.gameObject)
        {
            StartCoroutine(NewPlayer.Instance.Die());
            //NewPlayer.Instance.rope.hook.GetComponent<HookFollow>().follow = null;
        }
    }

}
