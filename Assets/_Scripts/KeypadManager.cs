using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadManager : MonoBehaviour
{
    public string password = "";
    public string current_password = "";

    public Material selected_material;
    public Material default_material;
    private Material old_material;
    private Transform _selection;
    public GameObject selector;

    public float delay = 1.0f;
    public float delay_temp;
    public Text text;

    public LineRenderer pointer;

    private void Start()
    {
        delay_temp = delay;
    }

    // Update is called once per frame
    void Update()
    {
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
        text.text = current_password;

        if (current_password.Length >= password.Length)
            delay_temp -= Time.deltaTime;

        if(delay_temp <= 0)
        {
            if (string.Compare(current_password, password) == 0)
                Debug.Log("Correct password!");
            else
                Debug.Log("Incorrect password :(");

            delay_temp = delay;
            current_password = "";
        }
    }
}
