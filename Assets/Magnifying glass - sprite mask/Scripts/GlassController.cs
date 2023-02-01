using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlassController : MonoBehaviour
{
    [Tooltip("Prefab to be used as a magnifying glass")]
    [SerializeField] GameObject glassPrefab;

    private GameObject glassPrefabInstance;

    void Update()
    {
        if (glassPrefab == null) return;
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (glassPrefabInstance == null)
                glassPrefabInstance = Instantiate(glassPrefab, FollowMouse.GetMouseWorldPos2D(), Quaternion.identity);
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            if (glassPrefabInstance)
            {
                Destroy(glassPrefabInstance);
            }
        }
    }
}
