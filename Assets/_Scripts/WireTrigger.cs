using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTrigger : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        // Debug.Log("Trigger detected");
        if (Input.GetMouseButtonDown(0) && other.tag == "Cutter")
        {
            Debug.Log("Cut detected");

            // instantiate particle system
            SparkAnimation spark = GetComponent<SparkAnimation>();
            spark.PlayAnimation();

            // send signal to collider
            if (transform.parent.GetComponent<InvisibleMaskObject>() != null && transform.parent.GetComponent<InvisibleMaskObject>().enabled)
            {
                // wire is fake
            }
            else
            {
                // wire is real
            }

            // destroy this object
            Destroy(gameObject);
        }
    }
}
