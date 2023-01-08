using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPylon : Pylon
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != NewPlayer.Instance.gameObject) return;
        if(!electrified) base.ConnectPylon();
        base.interactable = false;
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

    }

}
