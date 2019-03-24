using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StickSlot : MonoBehaviour
{
    BoxCollider2D stickCollider;
    BoxCollider2D stickSlot;
    EventSystem m_EventSystem;
    ShapeSlot shapeSlot;
    public List<GameObject> heldSticks;
    public Color topStickColor;

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
        SpriteRenderer currentStickSprite = currentStick.GetComponent<SpriteRenderer>();

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
                // If the slot is empty, simply add stick to slot. 
                if (heldSticks.Count == 0) 
                {
                    shapeSlot.AddToSidesFilled(true);
                    // Block moved outside of condition (below) to remove avoid code redundancy.
                } else if (heldSticks.Count == 1 ) // If the color is different than the held stick, mix them.
                {
                    Color heldStickColor = heldSticks[0].GetComponent<SpriteRenderer>().color;
                    if (currentStickSprite.color != heldStickColor) {
                        currentStick.GetComponent<SpriteRenderer>().color = MixColors(currentStickSprite.color, heldStickColor);
                        currentStickSprite.sortingOrder = 3;
                        shapeSlot.AddToSidesFilled(false);
                    } else // Early exit to avoid stick placement if the colors match.
                    {
                        return;
                    }
                } else // Avoid stick placement if there are 2 sticks already by returning from the function
                {
                    return;
                }
                Camera.main.GetComponent<DraggableObject2D>().draggingMode = false;
                currentStick.transform.position = gameObject.transform.position;
                currentStick.transform.eulerAngles = gameObject.transform.eulerAngles;
                heldSticks.Add(currentStick);
                topStickColor = currentStick.GetComponent<SpriteRenderer>().color;
                shapeSlot.CheckShapeCompleteness();
            }
        }
    }

    private Color MixColors(Color color1, Color color2) 
    {
        return Color.Lerp(color1, color2, 0.5f);
    }
}
