using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    public static int numberOfCorrectWires = 3;
    static List<GameObject> correctWires;

    void Start()
    {
        correctWires = new List<GameObject>();
    }

    public static void ManageCutWire(bool isFake, GameObject wire)
    {
        if (!isFake)
        {
            if (!correctWires.Contains(wire))
            {
                correctWires.Add(wire);
                if (correctWires.Count == numberOfCorrectWires)
                {
                    Debug.Log("Wire-cutting puzzle complete");
                }
            }
        }
        else
        {
            Debug.Log("Wrong wire cut");
        }
    }
}
