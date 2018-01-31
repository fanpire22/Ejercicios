using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform Target;
    public float Speed;
    
    private void LateUpdate()
    {
        if (!Target) return;

        Vector3 desiredPosition = Target.position;
        desiredPosition.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Speed * Time.deltaTime);
    }
}
