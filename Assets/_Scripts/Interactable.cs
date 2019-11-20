using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public void Pressed()
    {
        if (transform.CompareTag("Key"))
        {
            GameObject GameManager = GameObject.Find("GameManager");
            GameManager.GetComponent<KeypadManager>().KeyPressed();
        }
        /*
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        bool flip = !renderer.enabled;

        renderer.enabled = flip;
        */
    }
}
