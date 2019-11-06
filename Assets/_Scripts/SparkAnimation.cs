using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkAnimation : MonoBehaviour
{
    public GameObject SparkParticlePrefab;
    Rigidbody rb1;
    Rigidbody rb2;

    void Start()
    {
        HingeJoint joint = GetComponent<HingeJoint>();
        rb1 = joint.connectedBody;

        Rigidbody curr = GetComponent<Rigidbody>();
        foreach (Transform t in transform.parent)
        {
            joint = t.GetComponent<HingeJoint>();
            if (joint.connectedBody == curr)
            {
                rb2 = t.GetComponent<Rigidbody>();
            }
        }
    }

    public void PlayAnimation()
    {
        if (rb1 == null || rb2 == null)
            return;

        GameObject flip = Instantiate(SparkParticlePrefab, rb1.transform);
        flip.transform.localRotation = Quaternion.Euler(90, 0, 0);
        flip = Instantiate(SparkParticlePrefab, rb2.transform);
        flip.transform.localRotation = Quaternion.Euler(-90, 0, 0);

        rb1.AddExplosionForce(0.01f, new Vector3(0,0,0), 5.0f);
        rb2.AddExplosionForce(0.01f, new Vector3(0, 0, 0), 5.0f);
    }
}
