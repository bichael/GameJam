using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* This script allows an object to be draggable by holding left mouse button.
 * The dragging is only in 2 dimensions.
 * Script from https://www.youtube.com/watch?v=Be9v-sYO-Aw  */
public class DraggableObject2D : MonoBehaviour
{
    EventSystem m_EventSystem;
    public GameObject currentStick;
    public Vector3 stickCenter;  // GameObject center
    public Vector3 touchPosition;  // Touch (click) position
    public Vector3 offset;  // Vector between touch/click to object center
    public Vector3 newCenter;  // position to drop object
    public float velocity = 30.0f;

    RaycastHit hit;  // Determine if click finds object using ray

    public bool draggingMode = false;

    // Start is called before the first frame update
    void Start()
    {
        m_EventSystem = EventSystem.current;        
    }

    // Update is called once per frame
    void Update()
    {
        /*---- CLICK TO DRAG ----*/
#if UNITY_EDITOR
        // First frame when user clicks left mouse
        if (Input.GetMouseButtonDown(0)) 
        {
            // If a ray hit a collider (2D)            
            var hit2d = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit2d && (hit2d.collider.gameObject.tag == "Stick")) 
            {
                currentStick = hit2d.collider.gameObject;
                stickCenter = currentStick.transform.position; 
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
                offset = touchPosition - stickCenter;
                draggingMode = true; 
                m_EventSystem.SetSelectedGameObject(currentStick);
            }

            // // (3D)
            // // Convert moust click position to a ray
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // // If a ray hit a collider (3D)
            // if (Physics.Raycast(ray, out hit))
            // {
            //     // currentStick = hit.collider.gameObject;
            //     currentStick = hit.collider.gameObject;
            //     stickCenter = currentStick.transform.position;
            //     touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //     offset = touchPosition - stickCenter;
            //     draggingMode = true;
            // }
        }

        // Every frame when user holds on left mouse
        if (Input.GetMouseButton(0))
        {
            if (draggingMode)
            {
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newCenter = touchPosition - offset;
                currentStick.transform.position = new Vector3(newCenter.x, newCenter.y, newCenter.z);
            }
            
        }

        if (currentStick && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
        {
            if (currentStick == m_EventSystem.currentSelectedGameObject)
                currentStick.transform.Rotate(Vector3.forward * velocity * Time.deltaTime);
        }
        
        if (currentStick && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
        {
            if (currentStick == m_EventSystem.currentSelectedGameObject)
                currentStick.transform.Rotate(Vector3.back * velocity * Time.deltaTime);
        }

        // When mouse is released
        if (Input.GetMouseButtonUp(0))
        {
            draggingMode = false;
            currentStick = null;
        }
#endif
    }
}
