using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script allows an object to be draggable by holding left mouse button.
 * The dragging is only in 2 dimensions.
 * Script from https://www.youtube.com/watch?v=Be9v-sYO-Aw  */
public class DraggableObject : MonoBehaviour
{

    public GameObject currentStick;

    public Vector3 stickCenter;  // GameObject center
    public Vector3 touchPosition;  // Touch (click) position
    public Vector3 offset;  // Vector between touch/click to object center
    public Vector3 newCenter;  // position to drop object

    RaycastHit hit;  // Determine if click finds object using ray

    public bool draggingMode = false;

    // Start is called before the first frame update
    void Start()
    {
        
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

            if (hit2d) 
            {
                currentStick = hit2d.collider.gameObject; 
                stickCenter = currentStick.transform.position; 
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
                offset = touchPosition - stickCenter; 
                draggingMode = true; 
            }

            // (3D)
            // Convert moust click position to a ray
            // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // // If a ray hit a collider (3D)
            // if (Physics.Raycast(ray, out hit))
            // {
            //     currentStick = hit.collider.GameObject;
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

        // When mouse is released
        if (Input.GetMouseButtonUp(0))
        {
            draggingMode = false;
        }
#endif
    }
}
