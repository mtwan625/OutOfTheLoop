using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    int nextAlarm = 1;

    public Material offMaterial;
    public Material onMaterial;

    public GameObject alarm1;
    public GameObject alarm2;
    public GameObject alarm3;

    Material on;
    Material off;

    void Start()
    {
    	nextAlarm = 1;
        on = onMaterial;
        off = offMaterial;
    }

    public void triggerAlarm() {
    	switch (nextAlarm) {
    		case 1:
    			alarm1.GetComponent<Renderer>().material = on;
    			break;
    		case 2:
    			alarm2.GetComponent<Renderer>().material = on;
    			break;
    		case 3:
    			alarm3.GetComponent<Renderer>().material = on;
                // end game
    			break;
    		default:
    			break;
    	}
    	nextAlarm++;
    }

    public void disableAlarms() {
    	alarm1.GetComponent<Renderer>().material = off;
    	alarm2.GetComponent<Renderer>().material = off;
    	alarm3.GetComponent<Renderer>().material = off;
    	nextAlarm = 1;
    }
}
