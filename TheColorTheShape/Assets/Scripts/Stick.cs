using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public Color originalColor;
    public StickSlot parentSlot;
    // Start is called before the first frame update
    void Start()
    {
        originalColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParentSlot(StickSlot slot)
    {
        parentSlot = slot;
    }

    public StickSlot GetParentSlot()
    {
        return parentSlot; 
    }
}
