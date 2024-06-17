using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float moveSpeed = 10f;

    public float xOffset = 0f;
    public float yOffset;
    public float zOffset;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + xOffset, target.position.y + yOffset, target.position.z + zOffset), moveSpeed * Time.deltaTime);    
    }
}
