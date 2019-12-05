using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWire : MonoBehaviour
{
    public GameObject wireSegmentPrefab;
    public Material wireMaterial;
    public float thickness = 0.1f;
    public float slack = 3.0f;

    Rigidbody anchorA;
    Rigidbody anchorB;
    Rigidbody hook;

    List<GameObject> wireSegments;
    void Awake()
    {
        anchorA = transform.Find("Anchor A").GetComponent<Rigidbody>();
        anchorB = transform.Find("Anchor B").GetComponent<Rigidbody>();
        hook = transform.Find("Hook").GetComponent<Rigidbody>();

        wireSegments = new List<GameObject>();

        Generate();
    }

    void Generate()
    {
        float distance = Vector3.Distance(anchorA.transform.position, anchorB.transform.position);
        int segments = Mathf.RoundToInt(distance * slack);
        // Debug.Log("Number of segments: " + segments);

        Rigidbody previousRB = hook;
        for (int i = 0; i < segments; i++)
        {
            GameObject segment = Instantiate(wireSegmentPrefab, transform);
            wireSegments.Add(segment);
            HingeJoint joint = segment.GetComponent<HingeJoint>();
            LineRenderer renderer = segment.AddComponent<LineRenderer>();
            renderer.positionCount = 2;
            renderer.material = wireMaterial;
            renderer.widthMultiplier = thickness;
            renderer.generateLightingData = true;
            joint.connectedBody = previousRB;
            previousRB = segment.GetComponent<Rigidbody>();
        }

        HingeJoint finalJoint = anchorB.GetComponent<HingeJoint>();
        finalJoint.connectedBody = previousRB;
    }

    void Update()
    {
        foreach (GameObject s in wireSegments)
        {
            if (s == null)
                continue;
            HingeJoint joint = s.GetComponent<HingeJoint>();
            LineRenderer renderer = s.GetComponent<LineRenderer>();
            if (joint.connectedBody == null)
            {
                Destroy(renderer);
            }
            else
            {
                renderer.SetPosition(0, joint.connectedBody.transform.position);
                renderer.SetPosition(1, s.transform.position);
            }
        }
    }
}
