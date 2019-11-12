using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadManagement : MonoBehaviour
{
    public string password = "";
    public string current_password = "";

    public Material selected_material;
    private Material old_material;
    private Transform _selection;

    public float delay = 1.0f;
    public float delay_temp;
    public Text text;

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
                selectionRenderer.material = old_material;
            _selection = null;
        }
        
        // raycast to detect selection
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

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
                   
                if(Input.GetMouseButtonDown(0))
                {
                    GameObject key = _selection.gameObject;
                    KeypadInput input = key.GetComponent<KeypadInput>();

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
