using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    SpriteRenderer currentStickSprite;
    public float velocity = 30.0f;
    public GameObject[] slotGameObjects;

    RaycastHit hit;  // Determine if click finds object using ray

    public bool draggingMode = false;

    // Start is called before the first frame update
    void Start()
    {
        m_EventSystem = EventSystem.current;   
        slotGameObjects = GameObject.FindGameObjectsWithTag("Slot");
    }

    // Update is called once per frame
    void Update()
    {
        // Press R to restart a level
        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        /*---- CLICK TO DRAG ----*/
        // First frame when user clicks left mouse
        if (Input.GetMouseButtonDown(0)) 
        {
            // If a ray hit a collider (2D)            
            var hit2d = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit2d && (hit2d.collider.gameObject.tag == "Stick")) 
            {
                currentStick = hit2d.collider.gameObject;

                // Early exit from dragging if a stick is part of a completed shape.
                StickSlot stickParentSlot = currentStick.GetComponent<Stick>().GetParentSlot();
                if (stickParentSlot != null) 
                    if (stickParentSlot.GetShapeSlot().shapeComplete)
                        return;

                stickCenter = currentStick.transform.position; 
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
                offset = touchPosition - stickCenter;
                currentStickSprite = currentStick.GetComponent<SpriteRenderer>();
                draggingMode = true; 
                m_EventSystem.SetSelectedGameObject(currentStick);
            }
        }

        // Every frame when user holds on left mouse
        if (Input.GetMouseButton(0))
        {
            if (draggingMode)
            {
                // if stick is in a slot:  remove it from that slot now
                foreach (GameObject slot in slotGameObjects) 
                {
                    if (slot.GetComponent<StickSlot>().heldSticks.Contains(currentStick)) {
                        slot.GetComponent<StickSlot>().heldSticks.Remove(currentStick);
                        currentStick.GetComponent<Stick>().SetParentSlot(null);
                        Color originalColor = currentStick.GetComponent<Stick>().originalColor;
                        // If the stick being removed has a modified color (i.e. there is another stick under):
                        if (currentStickSprite.color != originalColor) 
                        {
                            ShapeSlot stickShape = slot.transform.parent.GetComponent<ShapeSlot>();
                            stickShape.RemoveFromSidesFilled(false);
                            currentStickSprite.color = originalColor;
                            currentStickSprite.sortingOrder = 2;
                            StickSlot currentStickSlot = slot.GetComponent<StickSlot>();
                            currentStickSlot.topStickColor = currentStickSlot.heldSticks[0].GetComponent<SpriteRenderer>().color;
                            stickShape.CheckShapeCompleteness();
                        } else {
                            slot.transform.parent.GetComponent<ShapeSlot>().RemoveFromSidesFilled(true);
                            slot.GetComponent<StickSlot>().topStickColor = Color.white;
                        }
                        // avoid unnecessary computations by exiting loop, since only 1 side can be removed at a time
                        // TODO change into a while loop?
                        break;
                    }
                }
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newCenter = touchPosition - offset;
                currentStick.transform.position = new Vector3(newCenter.x, newCenter.y, newCenter.z);
            }
            
        }

        if (currentStick && draggingMode && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
        {
            if (currentStick == m_EventSystem.currentSelectedGameObject)
                currentStick.transform.Rotate(Vector3.forward * velocity * Time.deltaTime);
        }
        
        if (currentStick && draggingMode && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
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
    }
}
