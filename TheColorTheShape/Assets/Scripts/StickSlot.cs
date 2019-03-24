using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StickSlot : MonoBehaviour
{
    BoxCollider2D stickCollider;
    BoxCollider2D stickSlot;
    EventSystem m_EventSystem;
    public Color desiredColor;
    ShapeSlot shapeSlot;
    public List<GameObject> heldSticks;

    // Start is called before the first frame update
    void Start()
    {
        m_EventSystem = EventSystem.current;   
        heldSticks = new List<GameObject>();
        stickSlot = GetComponent<BoxCollider2D>();
        shapeSlot = this.transform.parent.GetComponent<ShapeSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject currentStick = collision.gameObject;

        // Exit early if *somehow* an object that is not the object being dragged collides with a stickSlot
        if (currentStick != m_EventSystem.currentSelectedGameObject)
            return;

        // If the stick is close enough in the right x,y position
        if ((Mathf.Abs(currentStick.transform.position.x - gameObject.transform.position.x) < 1)
            && (Mathf.Abs(currentStick.transform.position.y - gameObject.transform.position.y) < 1)) 
            {
            // And the stick is rotated along the z axis similarly enough
            if (Mathf.Abs(currentStick.transform.rotation.z - gameObject.transform.rotation.z) < 10) 
            {
                // If the slot is empty, simply add stick to slot. // if slot.Count==1: mix colors and add 1 to sorting order.
                if (heldSticks.Count == 0) 
                {
                    // Hide current stick, change color of stickslot to take the color of the current stick.
                    // currentStick.SetActive(false);
                    // stickSlot.GetComponent<SpriteRenderer>().color = currentStickColor;
                    // currentStick.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                // Sorting order will keep sticks above slots in reference to the camera view.
                currentStick.transform.position = gameObject.transform.position;
                currentStick.transform.eulerAngles = gameObject.transform.eulerAngles;
                shapeSlot.AddToSidesFilled();
                heldSticks.Add(currentStick);
                } else if (heldSticks.Count == 1)
                {
                    Debug.Log("This is where we would blend colors.");
                    // Color currentStickColor = currentStick.GetComponent<SpriteRenderer>().color;
                }
            }
        }
    }
}
