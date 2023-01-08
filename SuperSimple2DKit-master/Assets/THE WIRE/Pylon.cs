using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pylon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        NewPlayer.Instance.rope.AttachDetachPylon(this);
    }
}
