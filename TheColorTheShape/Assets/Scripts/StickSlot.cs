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

    // Start is called before the first frame update
    void Start()
    {
        m_EventSystem = EventSystem.current;   
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

        Color currentStickColor = currentStick.GetComponent<SpriteRenderer>().color;
        // If the stick is the right color
        if (currentStickColor == desiredColor) {
            // And the stick is close enough in the right x,y position
            if ((Mathf.Abs(currentStick.transform.position.x - gameObject.transform.position.x) < 1)
              && (Mathf.Abs(currentStick.transform.position.y - gameObject.transform.position.y) < 1)) {
                // And the stick is rotated along the z axis similarly enough
                if (Mathf.Abs(currentStick.transform.rotation.z - gameObject.transform.rotation.z) < 10) {
                        // Hide current stick, change color of stickslot to take the color of the current stick.
                        // currentStick.SetActive(false);
                        // stickSlot.GetComponent<SpriteRenderer>().color = currentStickColor;
                        // currentStick.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                    // StickSlots have an added z value of .001 to have them "under" sticks, which is accounted for here
                    currentStick.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, (gameObject.transform.position.z - .001f));
                    currentStick.transform.eulerAngles = gameObject.transform.eulerAngles;
                    shapeSlot.AddToFilledCount(currentStick);
                }
            }
        }
    }
}
