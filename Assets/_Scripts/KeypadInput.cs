using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadInput : MonoBehaviour
{
    public string character;

    private RaycastHit hit;
    private Vector3 direction = Vector3.up;
    private float distance = 3;


    void Update()
    {
        Debug.DrawRay(transform.position, direction * 3, Color.green);

        if (Physics.Raycast(transform.position, direction, out hit, distance * 3))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
