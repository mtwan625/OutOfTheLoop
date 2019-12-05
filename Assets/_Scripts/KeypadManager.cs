using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadManager : MonoBehaviour
{
    public string password = "";
    private string current_password = "";

    public Material selected_material;
    public Material default_material;
    private Material old_material;
    private Transform _selection;

    public Pointer pointer;

    public float delay = 1.0f;
    private float delay_temp;

    public TextMeshPro text;

    private bool isCompleted = false;

    public GameObject wireDetector;
    public GameObject wireCutter;

    void Awake()
    {
        delay_temp = delay;
        EnableKeyInteraction();

        wireDetector.SetActive(false);
        wireCutter.SetActive(false);
    }

    void OnDestroy()
    {
        DisableKeyInteraction();
    }

    void Update()
    {
        if (isCompleted)
            return;

        #region preliminary code
        /*
        // deselection
        if(_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            if (selectionRenderer != null)
            {   
                if (old_material != null)
                    selectionRenderer.material = old_material;
                else
                    selectionRenderer.material = default_material;

            }
            

            GameObject key = _selection.gameObject;
            KeypadInput input = key.GetComponent<KeypadInput>();
            input.selected = false;

            _selection = null;
        }

        // raycast to detect selection
        ///Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        Ray ray = new Ray(selector.transform.position, selector.transform.forward);
        RaycastHit hit;
        // Debug.DrawRay(selector.transform.position, selector.transform.forward * 10, Color.green);

        //pointer.SetPosition(0, selector.transform.position);
        //pointer.SetPosition(1, selector.transform.position + selector.transform.forward * 10);


        if (Physics.Raycast(ray, out hit) && current_password.Length < password.Length)
        {
            _selection = hit.transform;
            if(_selection.CompareTag("Key"))
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();

                if(selectionRenderer != null)
                {
                    old_material = selectionRenderer.material;
                    selectionRenderer.material = selected_material;
                }

                //if(Input.GetMouseButtonDown(0))
                if(OVRInput.GetDown(OVRInput.Button.One))
                {
                    GameObject key = _selection.gameObject;
                    KeypadInput input = key.GetComponent<KeypadInput>();

                    input.selected = true;
                    current_password += input.character;
                }
            }
        }
        */
        #endregion

        text.text = current_password;

        if (current_password.Length >= password.Length)
        {
            delay_temp -= Time.deltaTime;
            UnhighlightKey();
            DisableKeyInteraction();
        }

        if(delay_temp <= 0)
        {
            if (string.Compare(current_password, password) == 0)
            {
                Debug.Log("Correct password!");
                wireDetector.SetActive(true);
                wireCutter.SetActive(true);
                isCompleted = true;
            }
            else
            {
                Debug.Log("Incorrect password :(");
                EnableKeyInteraction();
            }

            delay_temp = delay;
            current_password = "";
        }
    }

    void HighlightKey(GameObject hit)
    {
        _selection = hit.transform;
        if (_selection.CompareTag("Key"))
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();

            if (selectionRenderer != null)
            {
                old_material = selectionRenderer.material;
                selectionRenderer.material = selected_material;
            }
        }
    }

    void UnhighlightKey()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            if (selectionRenderer != null)
            {
                if (old_material != null)
                    selectionRenderer.material = old_material;
                else
                    selectionRenderer.material = default_material;
            }

            GameObject key = _selection.gameObject;
            KeypadInput input = key.GetComponent<KeypadInput>();
            input.selected = false;

            _selection = null;
        }
    }

    public void KeyPressed()
    {
        GameObject key = _selection.gameObject;
        KeypadInput input = key.GetComponent<KeypadInput>();

        input.selected = true;
        if (input.character == "D")
            current_password = current_password.Remove(current_password.Length - 1);
        else
            current_password += input.character;
    }
    
    void EnableKeyInteraction()
    {
        pointer.OnPointerHover += HighlightKey;
        pointer.OffPointerHover += UnhighlightKey;
    }

    void DisableKeyInteraction()
    {
        pointer.OnPointerHover -= HighlightKey;
        pointer.OffPointerHover -= UnhighlightKey;
    }
}
