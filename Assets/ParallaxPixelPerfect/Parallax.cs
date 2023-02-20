using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Tooltip("Negative - moves oposite the camera movement; Positive - moves in the camera direction.")]
    [Range(-1f,1f)]
    [SerializeField] float multiplier;

    // here we cache the reference to camera transform
    private Transform cam;

    // initial position of the object/plane to parallax
    private Vector3 objectStartPosition;

    private void OnEnable()
    {
        cam = Camera.main.transform;
        CinemachineCore.CameraUpdatedEvent.AddListener(ParallaxMove);
        objectStartPosition = GetPixelClampedMoveVector(transform.position, 64);
    }

    private void OnDisable()
    {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(ParallaxMove);
    }

    private void ParallaxMove(CinemachineBrain brain) 
    {
        Vector3 camPosition = new Vector3(brain.transform.position.x, 
                                            brain.transform.position.y, 
                                            transform.position.z);

        Vector3 cameraOffset = GetPixelClampedMoveVector(camPosition, 64);
        Vector3 pos = GetPixelClampedMoveVector(objectStartPosition + cameraOffset * multiplier, 64);
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }   

    public static Vector3 GetPixelClampedMoveVector(Vector3 moveVector, float pixelsPerUnit)
    {
        Vector3 vectorInPixels = new Vector3(
                Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
                Mathf.RoundToInt(moveVector.y * pixelsPerUnit),
                moveVector.z
            );
        Debug.Log(vectorInPixels);

        return new Vector3(vectorInPixels.x / pixelsPerUnit, vectorInPixels.y / pixelsPerUnit, vectorInPixels.z);
    }
}

