using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This will attach to a "Shape", which is a formation of StickSlots.
// This script enables Slots to accept Sticks, and display a message when all are filled.
public class ShapeSlot : MonoBehaviour
{
    List<GameObject> stickSlotsList;
    int maxSides;
    int sidesFilled;
    // Start is called before the first frame update
    void Start()
    {
        sidesFilled = 0;
        stickSlotsList = new List<GameObject>();
        // Get all children stick slots
        Transform t = gameObject.transform;
 
        for (int i = 0; i < t.childCount; i++) 
        {
            GameObject childGameObject = t.GetChild(i).gameObject;
            if(childGameObject.tag == "Slot")
                stickSlotsList.Add(childGameObject);
        }
        maxSides = stickSlotsList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToSidesFilled() {
        sidesFilled += 1;
        if (sidesFilled == maxSides) {
            // TODO do something cooler when a shape is completed.
            Debug.Log("Shape finished!");
        }
        Camera.main.GetComponent<DraggableObject2D>().draggingMode = false;
    }

    public void RemoveFromSidesFilled() {
        sidesFilled -= 1;
    }
}
