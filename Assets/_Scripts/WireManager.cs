using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    public int numberOfCorrectWires = 3;
    static int wireCount;
    static List<GameObject> correctWires;

    static AlarmManager alarmManager;

    void Start()
    {
        correctWires = new List<GameObject>();
        wireCount = numberOfCorrectWires;
        alarmManager = GetComponent<AlarmManager>();
    }

    public static void ManageCutWire(bool isFake, GameObject wire)
    {
        if (!isFake)
        {
            if (!correctWires.Contains(wire))
            {
                correctWires.Add(wire);
                if (correctWires.Count == wireCount)
                {
                    Debug.Log("Wire-cutting puzzle complete");
                    alarmManager.disableAlarms();
                }
            }
        }
        else
        {
            // Debug.Log("Wrong wire cut");
            alarmManager.triggerAlarm();
        }
    }
}
