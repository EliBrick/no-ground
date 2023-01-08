using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookFollow : MonoBehaviour
{
    public Transform follow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (follow is not null)
            transform.position = follow.position;
        else
        {
            //Debug.Log("follow is gone");
            GetComponent<Rigidbody2D>().mass = 100;
        }
            
    }
}
