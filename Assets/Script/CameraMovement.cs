using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
 public Transform cameraTarget;
    public float pLerp = .02f;
    public float rLerp = .01f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cameraTarget.position, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraTarget.rotation, rLerp);
    }

}
