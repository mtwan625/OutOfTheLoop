using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleMaskObject : MonoBehaviour
{
    void Start()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.material.renderQueue = 3002;
        }
    }
}
