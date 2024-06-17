using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float width;

    public bool canFall = true;
    public bool canShowShadow = true;
    
    public float y => transform.position.y;
    public float left => transform.position.x - width / 2f;
    public float right => transform.position.x + width / 2f;

    private void Awake()
    {
        PlatformDataMgr.Instance.AddPlatform(this);
    }

    public bool IsOnMe(Vector3 pos)
    {
        if (pos.y >= y && pos.x >= left && pos.x <= right)
            return true;
        else
            return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.left * width / 2, transform.position + Vector3.right * width / 2);
    }
}
