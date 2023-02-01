using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 transformedPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(transformedPos.x, transformedPos.y, 0f);      
    }
}
