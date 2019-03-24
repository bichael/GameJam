using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.EventSystems;

// This will attach to a "Shape", which is a formation of StickSlots.
// This script enables Slots to accept Sticks, and display a message when all are filled.
public class ShapeSlot : MonoBehaviour
{
    // EventSystem m_EventSystem;
    List<GameObject> stickSlotsList;
    int sideCount;
    int filledCount;
    public GameObject droppedStick;
    // Start is called before the first frame update
    void Start()
    {
        filledCount = 0;
        stickSlotsList = new List<GameObject>();
        // Get all children stick slots
        Transform t = gameObject.transform;
 
        for (int i = 0; i < t.childCount; i++) 
        {
            GameObject childGameObject = t.GetChild(i).gameObject;
            if(childGameObject.tag == "Slot")
                stickSlotsList.Add(childGameObject);
                // return t.GetChild(i).gameObject;
        }
        sideCount = stickSlotsList.Count;
        // m_EventSystem = EventSystem.current;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToFilledCount(GameObject currentStick) {
        filledCount += 1;
        if (filledCount == sideCount) {
            // TODO do something cooler when a shape is completed.
            Debug.Log("Shape finished!");
        }
        droppedStick = currentStick;
        Camera.main.GetComponent<DraggableObject2D>().draggingMode = false;
    }
}
