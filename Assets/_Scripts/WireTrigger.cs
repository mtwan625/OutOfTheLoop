using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireTrigger : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        // Debug.Log("Trigger detected");
        CutterAnimator animator = other.transform.parent.GetComponent<CutterAnimator>();
        if (other.tag == "Cutter" && animator != null && animator.isAnimating)
        {
            Debug.Log("Cut detected");

            // instantiate particle system
            SparkAnimation spark = GetComponent<SparkAnimation>();
            spark.PlayAnimation();

            // send signal to manager
            bool isFake = transform.parent.GetComponent<InvisibleMaskObject>() != null && transform.parent.GetComponent<InvisibleMaskObject>().enabled;
            // Debug.Log("is wire cut fake? " + isFake);
            WireManager.ManageCutWire(isFake, transform.parent.gameObject);

            // destroy this object
            Destroy(gameObject);
        }
    }
}
