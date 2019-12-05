using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Transform holder;
    public GameObject pointer;
    public bool held = false;

    Vector3 startPosition;
    Vector3 startForward;

    LineRenderer line;

    void Start()
    {
        startPosition = transform.position;
        startForward = transform.forward;

        line = pointer.GetComponentInChildren<LineRenderer>();
        holder.position = line.GetPosition(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(held)
        {
            holder.position = line.GetPosition(0) + (line.GetPosition(1) - line.GetPosition(0)).normalized;

            if (holder != null)
            {
                transform.position += (holder.position - transform.position) / 10;
                transform.forward += ((line.GetPosition(1) - line.GetPosition(0)).normalized - transform.forward) / 10;
            }
        }
        else
        {
            transform.position += (startPosition - transform.position) / 10;
            transform.forward += (startForward - transform.forward) / 10;
        }

        // Debug.Log(transform.position);
        // Debug.Log(transform.rotation);
    }
}