using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManagerScript : MonoBehaviour
{
    bool alarm1On = false;
    bool alarm2On = false;
    bool alarm3On = false;

    // Start is called before the first frame update
    void Start()
    {
        int nextAlarm = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void triggerAlarm() {
    	switch (nextAlarm) {
    		case 1:
    			alarm1On = true;
    			Alarm1.GetComponent<Renderer>().material = AlarmOn;
    		case 2:
    			alarm2On = true;
    			Alarm2.GetComponent<Renderer>().material = AlarmOn;
    		case 3:
    			alarm3On = true;
    			Alarm3.GetComponent<Renderer>().material = AlarmOn;
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
    	Alarm1.GetComponent<Renderer>().material = AlarmOff;
    	Alarm2.GetComponent<Renderer>().material = AlarmOff;
    	Alarm3.GetComponent<Renderer>().material = AlarmOff;
    	nextAlarm = 1;
    }
}
