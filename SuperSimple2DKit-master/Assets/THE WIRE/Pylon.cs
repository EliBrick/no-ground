using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon : MonoBehaviour
{
    public RopeSegment attachedRopeSegment;
    public bool electrified = false;
    private bool interactable;
    SpriteRenderer sr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != NewPlayer.Instance.gameObject) return;
        //Debug.Log("yeap");
        interactable = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != NewPlayer.Instance.gameObject) return;
        //Debug.Log("nope");
        interactable = false;
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (interactable)
        {
            sr.color = Color.green;
        }
        else
        {
            sr.color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.E) && interactable)
        {
            ConnectPylon();
        }
    }

    void ConnectPylon()
    {
        attachedRopeSegment = NewPlayer.Instance.rope.AttachDetachPylon(this);
        electrified = (attachedRopeSegment is not null);
        GameManager.Instance.checkPuzzle();
    }
}
