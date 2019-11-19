using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadInput : MonoBehaviour
{
    public string character;
    public bool selected = false;

    private RaycastHit hit;
    private Vector3 direction = Vector3.up;
    private float distance = 3;

    public float pressed_distance = 0.3f;
    private Vector3 start_pos;

    private void Start()
    {
        start_pos = transform.position;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, direction * 3, Color.green);

        if (Physics.Raycast(transform.position, direction, out hit, distance * 3))
        {
            Debug.Log(hit.transform.name);
        }

        
        if (selected)
            Vector3.Lerp(transform.position, start_pos + transform.forward.normalized * pressed_distance, 0.1f);
        else
            Vector3.Lerp(transform.position, start_pos, 0.1f);
        
    }
}
