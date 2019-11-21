using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    public int numberOfCorrectWires = 3;
    public GameObject cutter;
    public float sensitivity = 2.0f;

    public bool isHolding; // currently holding cutter
    public bool isCutting; // currently cutting

    static int wireCount;
    static List<GameObject> correctWires;
    static List<GameObject> wiresCut;

    static AlarmManager alarmManager;

    void Start()
    {
        correctWires = new List<GameObject>();
        wiresCut = new List<GameObject>();
        wireCount = numberOfCorrectWires;
        alarmManager = GetComponent<AlarmManager>();
    }

    void Awake()
    {
        PlayerEvents.OnTriggerDown += Cut;
    }

    void OnDestroy()
    {
        PlayerEvents.OnTriggerDown -= Cut;
    }

    void Update()
    {
        // TODO: if isHolding, allow z-control with trackpad
        if (isHolding)
        {
            Vector2 move = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            cutter.transform.Translate(Vector3.forward * sensitivity * move.y * Time.deltaTime);
        }
    }

    public static void ManageCutWire(bool isFake, GameObject wire)
    {
        if (wiresCut.Contains(wire))
            return;
        wiresCut.Add(wire);

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

    public void PickUpCutter()
    {
        // TODO: Move "cutter" gameobject to hand
        isHolding = true;
    }

    public void DropCutter()
    {
        // TODO: Drop "cutter"
        isHolding = false;
    }

    void Cut()
    {
        isCutting = true;
    }
}
