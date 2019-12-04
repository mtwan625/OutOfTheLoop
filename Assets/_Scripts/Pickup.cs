using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject holder;
    public bool held = false;

    public Transform start;

    private void Start()
    {
        start = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(held)
        {
            if(holder != null)
            {
                transform.position += (holder.transform.position - transform.position) / 10;
                transform.forward += (holder.transform.forward - transform.forward) / 10;
            }
        }
        else
        {
            transform.position += (start.position - transform.position) / 10;
            transform.forward += (start.forward - transform.forward) / 10;
        }

        Debug.Log(transform.position);
        Debug.Log(transform.rotation);
    }
}