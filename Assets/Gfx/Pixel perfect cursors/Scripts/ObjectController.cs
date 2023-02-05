using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Interfaces IPointer*Handler require to have a Physics2DRaycaster (for 2D objecst)
// to be added to the main camera game object. Without it, the events will not fire.
public class ObjectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Color colorToChange = Color.red;
    
    private Color original;

    void Awake()
    {
        original = GetComponent<SpriteRenderer>().color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter using Physics 2D Raycaster.");
        GetComponent<SpriteRenderer>().color = colorToChange;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit using Physics 2D Raycaster.");
        GetComponent<SpriteRenderer>().color = original;
    }

    void OnMouseEnter()
    {
        // this event is fired by the old input system
        Debug.Log("Mouse enter using Old input handling.");
    }

    void OnMouseExit()
    {
        // this event is fired by the old input system
        Debug.Log("Mouse exit using Old input handling.");
    }

}
