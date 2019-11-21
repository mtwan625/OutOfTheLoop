using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    public static UnityAction OnTouchpadUp = null;
    public static UnityAction OnTouchpadDown = null;
    public static UnityAction OnTriggerDown = null;
    public static UnityAction OnTriggerUp = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;

    public GameObject m_RightAnchor;

    private OVRInput.Controller m_InputSource = OVRInput.Controller.None;
    private OVRInput.Controller m_Controller = OVRInput.Controller.None;
    private bool m_InputActive = true;

    void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;
    }

    void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;
    }

    void Update()
    {
        // Check for active input
        if (!m_InputActive)
            return;

        // Check if a controller exists
        CheckForController();

        // Check for input source
        CheckInputSource();

        // Check for actual input
        Input();
    }

    void CheckForController()
    {
        OVRInput.Controller controllerCheck = m_Controller;

        // Right remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            controllerCheck = OVRInput.Controller.RTrackedRemote;

        m_Controller = UpdateSource(controllerCheck, m_Controller);
    }

    void CheckInputSource()
    {
        m_InputSource = UpdateSource(OVRInput.GetActiveController(), m_InputSource);
    }

    void Input()
    {
        // Touchpad down
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            if (OnTouchpadDown != null)
                OnTouchpadDown();
        }

        // Touchpad up
        if (OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad))
        {
            if (OnTouchpadUp != null)
                OnTouchpadUp();
        }

        // Trigger down
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (OnTriggerDown != null)
                OnTriggerDown();
        }

        // Trigger up
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (OnTriggerUp != null)
                OnTriggerUp();
        }
    }

    OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {
        if (check == previous)
            return previous;

        GameObject controllerObject = m_RightAnchor;

        if (OnControllerSource != null)
            OnControllerSource(check, controllerObject);

        return check;
    }

    void PlayerFound()
    {
        m_InputActive = true;
    }

    void PlayerLost()
    {
        m_InputActive = false;
    }
}
