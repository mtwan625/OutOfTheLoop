using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    public float m_Distance = 10.0f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask = 0;
    public LayerMask m_InteractableMask = 0;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;
    public UnityAction<GameObject> OnPointerHover = null;
    public UnityAction OffPointerHover = null;

    Transform m_CurrentOrigin = null;
    GameObject m_CurrentObject = null;

    bool onHover = false;

    void Awake()
    {
        PlayerEvents.OnControllerSource += UpdateOrigin;
        PlayerEvents.OnTouchpadDown += ProcessTouchpadDown;
        PlayerEvents.OnTouchpadUp += ProcessTouchpadUp;
    }

    void Start()
    {
        SetLineColor();
    }

    void OnDestroy()
    {
        PlayerEvents.OnControllerSource -= UpdateOrigin;
        PlayerEvents.OnTouchpadDown -= ProcessTouchpadDown;
        PlayerEvents.OnTouchpadUp -= ProcessTouchpadUp;
    }

    void Update()
    {
        Vector3 hitPoint = UpdateLine();
        
        m_CurrentObject = UpdatePointerStatus();

        if (OnPointerUpdate != null)
            OnPointerUpdate(hitPoint, m_CurrentObject);
    }

    Vector3 UpdateLine()
    {
        // Create ray
        RaycastHit hit = CreateRaycast(m_EverythingMask);

        // Default end
        Vector3 endPosition = m_CurrentOrigin.position + (m_CurrentOrigin.forward * m_Distance);

        // Check hit
        if (hit.collider != null)
        {
            endPosition = hit.point;
            if (hit.transform.CompareTag("Key") && OnPointerHover != null && !onHover) // add more key tags here for on hover effects with pointer
            {
                OnPointerHover(hit.collider.gameObject);
                onHover = true;
            }
            else if (!hit.transform.CompareTag("Key") && OnPointerHover != null && onHover)
            {
                OffPointerHover();
                onHover = false;
            }
        }
        else if (OnPointerHover != null && onHover)
        {
            OffPointerHover();
            onHover = false;
        }

        // Set position
        m_LineRenderer.SetPosition(0, m_CurrentOrigin.position);
        m_LineRenderer.SetPosition(1, endPosition);

        return endPosition;
    }

    void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {
        m_CurrentOrigin = controllerObject.transform;
    }

    GameObject UpdatePointerStatus()
    {
        // Create ray
        RaycastHit hit = CreateRaycast(m_InteractableMask);

        // Check hit
        if (hit.collider)
            return hit.collider.gameObject;

        return null;
    }

    RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
        Physics.Raycast(ray, out hit, m_Distance, layer);

        return hit;
    }

    void SetLineColor()
    {
        if (!m_LineRenderer)
            return;

        Color endColor = Color.white;
        endColor.a = 0.0f;

        m_LineRenderer.endColor = endColor;
    }

    void ProcessTouchpadDown()
    {
        if (!m_CurrentObject)
            return;

        Interactable interactable = m_CurrentObject.GetComponent<Interactable>();
        interactable.Pressed();
    }

    void ProcessTouchpadUp()
    {
        if (!m_CurrentObject)
            return;

        Interactable interactable = m_CurrentObject.GetComponent<Interactable>();
        interactable.Released();
    }
}