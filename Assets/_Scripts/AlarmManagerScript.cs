using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManagerScript : MonoBehaviour
{
    bool alarm1On = false;
    bool alarm2On = false;
    bool alarm3On = false;
    int nextAlarm = 1;

    public GameObject alarm1;
    public GameObject alarm2;
    public GameObject alarm3;

    Material on;
    Material off;

    // Start is called before the first frame update
    void Start()
    {
        alarm1On = false;
    	alarm2On = false;
    	alarm3On = false;
    	nextAlarm = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void triggerAlarm() {
    	switch (nextAlarm) {
    		case 1:
    			alarm1On = true;
    			alarm1.GetComponent<Renderer>().material = on;
    			break;
    		case 2:
    			alarm2On = true;
    			alarm2.GetComponent<Renderer>().material = on;
    			break;
    		case 3:
    			alarm3On = true;
    			alarm3.GetComponent<Renderer>().material = on;
    			break;
    		default:
    			break;
    	}
    	nextAlarm++;
    	// if (alarm1On && alarm2On && alarm3On) { play alarm sound? }
    }

    void disableAlarms() {
    	alarm1On = false;
    	alarm2On = false;
    	alarm3On = false;
    	alarm1.GetComponent<Renderer>().material = off;
    	alarm2.GetComponent<Renderer>().material = off;
    	alarm3.GetComponent<Renderer>().material = off;
    	nextAlarm = 1;
    }
}
