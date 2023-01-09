using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon : MonoBehaviour
{
    public RopeSegment attachedRopeSegment;
    public bool electrified = false;
    protected bool interactable;
    protected SpriteRenderer sr;

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

    protected void ConnectPylon()
    {
        StartCoroutine(MovePlayerToPylon());
        
    }

    public IEnumerator MovePlayerToPylon()
    {
        Transform player = NewPlayer.Instance.gameObject.transform;
        BlockableInput.blockInput = true;
        while(Mathf.Abs(player.position.x-transform.position.x)>.5f)
        {
            player.position = Vector3.Lerp(player.position, new Vector3(transform.position.x, player.position.y, player.position.z), .05f);
            yield return null;
        }
        BlockableInput.blockInput = false;
        attachedRopeSegment = NewPlayer.Instance.rope.AttachDetachPylon(this);
        electrified = (attachedRopeSegment is not null);
        GameManager.Instance.checkPuzzle();
    }
}
