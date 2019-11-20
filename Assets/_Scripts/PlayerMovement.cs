using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject cameraRig;
    public GameObject centerEye;
    
    void Update()
    {
        Vector2 move = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        transform.eulerAngles = new Vector3(0, centerEye.transform.localEulerAngles.y, 0);
        transform.Translate(Vector3.forward * speed * move.y * Time.deltaTime);
        transform.Translate(Vector3.right * speed * move.x * Time.deltaTime);
        
        cameraRig.transform.position = transform.position;
    }
}
