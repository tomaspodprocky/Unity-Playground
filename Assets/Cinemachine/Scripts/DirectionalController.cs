using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectionalController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [Tooltip("Set this to the object CM will follow")]
    [SerializeField] Transform followObject;
    [Tooltip("Follow ahead offset applied to the object depending on the left/right movement")]
    [SerializeField] Vector2 followXOffset = new Vector2(1f, 0f);

    void OnEnable()
    {
        CinemachineVirtualCamera vCam = FindAnyObjectByType<CinemachineVirtualCamera>();
        vCam.Follow = followObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            followObject.localPosition = new Vector3(-followXOffset.x, -followXOffset.y, 0f);
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            followObject.localPosition = new Vector3(followXOffset.x, followXOffset.y, 0f);
        }
    }

}
