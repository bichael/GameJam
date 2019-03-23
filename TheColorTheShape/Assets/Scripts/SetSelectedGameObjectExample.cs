//This script shows how you set a non-UI GameObject as the current selected in the EventSystem by clicking on it.
//Make sure to assign a PhysicsRaycaster component to your Main Camera.
//Make sure your Scene has an EventSystem

//Attach this script to a GameObject. Create some other GameObjects to test on.
//In Play Mode, click on GameObjects to see the current selected be output to the console

using UnityEngine;
using UnityEngine.EventSystems;

public class SetSelectedGameObjectExample : MonoBehaviour
{
    EventSystem m_EventSystem;

    void OnEnable()
    {
        //Fetch the current EventSystem. Make sure your Scene has one.
        m_EventSystem = EventSystem.current;
    }

    void Update()
    {
        //Check if there is a mouse click
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //Send a ray from the camera to the mouseposition
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Create a raycast from the Camera and output anything it hits
            if (Physics.Raycast(ray, out hit))
                //Check the hit GameObject has a Collider
                if (hit.collider != null)
                {
                    //Click a GameObject to return that GameObject your mouse pointer hit
                    GameObject m_MyGameObject = hit.collider.gameObject;
                    //Set this GameObject you clicked as the currently selected in the EventSystem
                    m_EventSystem.SetSelectedGameObject(m_MyGameObject);
                    //Output the current selected GameObject's name to the console
                    Debug.Log("Current selected GameObject : " + m_EventSystem.currentSelectedGameObject);
                }
        }
    }
}