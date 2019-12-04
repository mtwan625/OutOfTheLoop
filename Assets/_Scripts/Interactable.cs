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

        if (transform.CompareTag("Cutter"))
        {
            GameObject GameManager = GameObject.Find("GameManager");
            GameManager.GetComponent<WireManager>().PickUpCutter();
        }

        if (transform.CompareTag("Pickup"))
        {
            this.gameObject.GetComponent<Pickup>().held = true;
        }
        /*
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        bool flip = !renderer.enabled;

        renderer.enabled = flip;
        */
    }

    public void Released()
    {
        if (transform.CompareTag("Cutter"))
        {
            GameObject GameManager = GameObject.Find("GameManager");
            GameManager.GetComponent<WireManager>().DropCutter();
        }

        /*
        if(transform.CompareTag("Pickup"))
        {
            this.gameObject.GetComponent<Pickup>().held = false;
        }
        */
    }
}
