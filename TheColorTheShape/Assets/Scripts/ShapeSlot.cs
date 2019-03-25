﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This will attach to a "Shape", which is a formation of StickSlots.
// This script enables Slots to accept Sticks, and display a message when all are filled.
public class ShapeSlot : MonoBehaviour
{
    public Color desiredColor;
    public bool shapeComplete;
    List<GameObject> stickSlotsList;
    int maxSides;
    int sidesFilled;
    public GameObject shapeInUI;
    public GameObject victoryPanel;
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

    public void AddToSidesFilled(bool increment) {
        if (increment)
            sidesFilled += 1;
    }

    public void RemoveFromSidesFilled(bool decrement) {
        if (decrement)
            sidesFilled -= 1;
    }

    public void CheckShapeCompleteness()
    {
        Debug.Log("checking if shape complete");
        if (sidesFilled == maxSides) {
            foreach (GameObject slot in stickSlotsList)
            {
                if (slot.GetComponent<StickSlot>().topStickColor != desiredColor)
                {
                    Debug.Log("topStickColor: " + slot.GetComponent<StickSlot>().topStickColor + ", desired = " + desiredColor);
                    return;
                }
            }
            shapeComplete = true;
            if (shapeInUI != null) {
                shapeInUI.SetActive(false);
            }
            // If all objects tagged "ShapesToBeMade" are null:  VICTORY!
            // FindObjectsWithTag ignores inactive GameObjects, which is desied.
            List<GameObject> shapesLeftToBeMade = new List<GameObject>(GameObject.FindGameObjectsWithTag("Shape"));
            if (shapesLeftToBeMade.Count == 0)
            {
                victoryPanel.SetActive(true);
                // TODO VictoryPopup.SetActive(true)
                // --> Contains "Next level" panel
                Debug.Log("VICTORY.");
            }
        }
    }
}
