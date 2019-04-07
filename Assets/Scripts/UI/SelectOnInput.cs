using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{

    public EventSystem eventSystem;

    public GameObject selectedObject;


    public bool buttonSelected = false;

    private void Awake()
    {
        eventSystem.SetSelectedGameObject(selectedObject);
        buttonSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }
}
